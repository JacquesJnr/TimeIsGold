using UnityEngine;
using Random = UnityEngine.Random;

namespace TarodevController {
    /// <summary>
    /// Basically a juice factory.
    /// </summary>
    
    public class PlayerAnimator : MonoBehaviour {
        [SerializeField] private Animator _anim;
        [SerializeField] private AudioSource _source;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private ParticleSystem _jumpParticles, _launchParticles;
        [SerializeField] private ParticleSystem _moveParticles, _landParticles;
        [SerializeField] private AudioClip[] _footsteps;
        [SerializeField] private AudioClip _kick;
        [SerializeField] private AudioClip _jump;
        [SerializeField] private float _maxTilt = .1f;
        [SerializeField] private float _tiltSpeed = 1;
        [SerializeField, Range(1f, 3f)] private float _maxIdleSpeed = 2;
        [SerializeField] private float _maxParticleFallSpeed = -40;

        private IPlayerController _player;
        private bool _playerGrounded;
        private ParticleSystem.MinMaxGradient _currentGradient;
        private Vector2 _movement;

        void Awake() => _player = GetComponentInParent<IPlayerController>();

        void Update() {
            if (StateMachine._gameState == GameStates.Menu) return;

            // Kick
            if (_player.KickingThisFrame)
            {
                _anim.SetTrigger(KickKey);
                _source.PlayOneShot(_kick);
            }
            
            // Flip the sprite
            if (_player.Input.X != 0) transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);

            // Lean while running
            var targetRotVector = new Vector3(0, 0, Mathf.Lerp(-_maxTilt, _maxTilt, Mathf.InverseLerp(-1, 1, _player.Input.X)));
            transform.rotation = 
                Quaternion.RotateTowards
                    (transform.rotation, Quaternion.Euler(targetRotVector), _tiltSpeed * Time.deltaTime);

            // Speed up idle while running
            _anim.SetFloat(IdleSpeedKey, Mathf.Lerp(1, _maxIdleSpeed, Mathf.Abs(_player.Input.X)));

            // Splat
            if (_player.LandingThisFrame) {
                _anim.SetTrigger(GroundedKey);
                _source.PlayOneShot(_footsteps[Random.Range(0, _footsteps.Length)]);
            }
            
            // Jump effects
            if (_player.JumpingThisFrame) {
                _anim.ResetTrigger(GroundedKey);
                _jumpParticles.Play();
                _source.PlayOneShot(_jump);

                // Only play particles when grounded (avoid coyote)
                if (_player.Grounded) {
                    SetColor(_jumpParticles);
                    SetColor(_launchParticles);
                    _source.pitch = 1;
                }

                if (!_player.Grounded)
                {
                    _source.pitch = 2;
                }
                _anim.SetTrigger(JumpKey);
            }

            // Play landing effects and begin ground movement effects
            if (!_playerGrounded && _player.Grounded) {
                _playerGrounded = true;
                _moveParticles.Play();
                _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, _maxParticleFallSpeed, _movement.y);
                SetColor(_landParticles);
                _landParticles.Play();
            }
            else if (_playerGrounded && !_player.Grounded) {
                _playerGrounded = false;
                _moveParticles.Stop();
            }

            // Detect ground color
            var groundHit = Physics2D.Raycast(transform.position, Vector3.down, 2, _groundMask);
            if (groundHit && groundHit.transform.TryGetComponent(out SpriteRenderer r)) {
                _currentGradient = new ParticleSystem.MinMaxGradient(r.color * 0.9f, r.color * 1.2f);
                SetColor(_moveParticles);
            }

            _movement = _player.RawMovement; // Previous frame movement is more valuable
        }

        private void OnDisable() {
            _moveParticles.Stop();
        }

        private void OnEnable() {
            _moveParticles.Play();
        }

        void SetColor(ParticleSystem ps) {
            var main = ps.main;
            main.startColor = _currentGradient;
        }

        #region Animation Keys

        private static readonly int GroundedKey = Animator.StringToHash("Grounded");
        private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
        private static readonly int JumpKey = Animator.StringToHash("Jump");
        private static readonly int KickKey = Animator.StringToHash("Kick");

        #endregion
    }
}