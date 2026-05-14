using System.Collections;
using _03_Code.Items;
using _03_Code.Items.Weapons;
using _03_Code.Player.Components;
using _03_Code.Player.Input;
using _03_Code.Player.Interface;
using UnityEngine;

//hyunwoo don't touch this script
namespace _03_Code.Player.Main
{
    public class PlayerControl : MonoBehaviour, IPlayerModule
    {
        [Header("Components")]
        [SerializeField] private AnimationControl ani;
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private ParticleSystem dashVfx;
        
        [Header("Settings")]
        [SerializeField] private float speed = 8f;
        [SerializeField] private float jumpForce;
        [SerializeField] private float dashForce = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCoolTime = 1f;
        
        //private TestDamageable[] testDam;

        private Player _owner;
        private Rigidbody2D _rb;
        private InputReceiver _input;
        private float _moveInput;


        public float MoveInput { get; private set; }

        
        private bool _rotationRight = true;
        private bool _isDashing; 
        private bool _isDashingCooltime;
        
        public void Initialize(Player owner) {
            _owner = owner;
            _rb = owner.GetComponent<Rigidbody2D>();
            _input = _owner.GetModule<InputReceiver>();
            _owner.GetModule<AnimationControl>();
            _input.OnJumpInput += HandleJump;
            _input.OnRunInput += HandleRun;
            _input.OnMoveInput += HandleMoveInput;
            _input.OnDashInput += HandleDashInput;
            _input.OnGuardInput += HandleGuard;
        }
        private void FixedUpdate() {
            if(_isDashing || GameManager.Instance.playerHit.IsApproach) return;
            _rb.linearVelocityX = _moveInput * speed;
            ani.OnMoveAni(Mathf.Abs(_moveInput));
            if (!Mathf.Approximately(_moveInput, 0f)) {
                _rotationRight = _moveInput > 0f;
                _owner.transform.rotation = Quaternion.Euler(0f, _rotationRight ? 0f : 180f, 0f);
            }
        }
        private void OnDestroy() {
            _input.OnMoveInput -= HandleMoveInput;
            _input.OnRunInput -= HandleRun;
            _input.OnJumpInput -= HandleJump;
            _input.OnDashInput -= HandleDashInput;
            _input.OnGuardInput -= HandleGuard;
        }

        private void HandleGuard() {
            //what is this?
            Destroy(gameObject);
            Time.timeScale = 0.2f;
        }
        
        private void HandleMoveInput(float value) {
            _moveInput = value;
            
        }


        private void HandleRun(bool run) {
            speed *= run ? 3/2f : 2/3f;
        }

        private void HandleJump() {
            const float multiplier = 3f;
            if (contactChecker.IsGrounded)
            {
                _rb.linearVelocity = Vector2.zero;
                _rb.AddForceY(jumpForce * multiplier, ForceMode2D.Impulse);
            }
        }
        
        private void HandleDashInput() {
            if (_isDashingCooltime) return;
            StartCoroutine(Dash());
            dashVfx?.Play();
        }

        private IEnumerator Dash() {
            _isDashing = true;
            _isDashingCooltime = true;
            ani.OnDashAni(true);
            var dashDirection = _rotationRight ? 2f : -2f;
            _rb.linearVelocity = new Vector2(dashDirection * dashForce, 0f);

            yield return new WaitForSeconds(dashDuration);
            
            ani.OnDashAni(false);
            _rb.linearVelocity = Vector2.zero;
            _isDashing = false;
            
            float cooltime = dashCoolTime - dashDuration > 0 ? dashCoolTime - dashDuration : 0f;
            yield return new WaitForSeconds(cooltime);
            
            _isDashingCooltime = false;
        }

    }
}
