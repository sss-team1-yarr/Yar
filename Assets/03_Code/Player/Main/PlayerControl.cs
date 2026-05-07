using System.Collections;
using _03_Code.Items;
using _03_Code.Items.Weapons;
using _03_Code.Player.Components;
using _03_Code.Player.Input;
using _03_Code.Player.Interface;
using Unity.Cinemachine;
using UnityEngine;

//hyunwoo don't touch this script
namespace _03_Code.Player.Main
{
    public class PlayerControl : MonoBehaviour, IPlayerModule
    {
        [SerializeField] private int explosion;
        [SerializeField] private float jumpForce;
        [SerializeField] private AnimationControl ani;
        [SerializeField] private HpManager hp;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private GameObject vfxBoom;
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [SerializeField] private Sword sword;
        [SerializeField] private float skill3ActiveTime;
        
        

        [field: SerializeField] public Weapon HoldingItem { get; private set; }
        //private TestDamageable[] testDam;

        private Player _owner;
        private Rigidbody2D _rb;
        private InputReceiver _input;

        public bool rotationRight;
        public bool isDashing;
        public bool IsFFF { get; private set; }
        public float speed = 8f;
        public float MoveInput { get; private set; }
        
        [SerializeField] private float dashForce = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private ParticleSystem dashVfx;

        public void Initialize(Player owner) {
            _owner = owner;
            _rb = owner.GetComponent<Rigidbody2D>();
            _input = _owner.GetModule<InputReceiver>();
            _owner.GetModule<AnimationControl>();
            _input.OnJumpInput += HandleJump;
            _input.OnRunInput += HandleRun;
            _input.OnMoveInput += HandleMoveInput;
            _input.OnAttackInput += HandleAttackInput;
            _input.OnSkill1Input += HandleSkill1Input;
            _input.OnSkill2Input += HandleSkill2Input;
            _input.OnGuardInput += HandleGuard;
            _input.OnSkill3Input += HandleSkill3Input;
        }

        private void HandleGuard() {
            //what is this?
            Destroy(gameObject);
            Time.timeScale = 0.2f;
        }

        private void Awake() {
            HoldingItem?.HoldItem(new ItemUsingContext { User = _owner, Input = 0, Pressed = true });
        }
        
        private void HandleMoveInput(float value) {
            _rb.linearVelocityX = value * speed;
            ani.OnMoveAni(Mathf.Abs(value));
            if (!Mathf.Approximately(value, 0f)) {
                rotationRight = value > 0f;
                _owner.transform.rotation = Quaternion.Euler(0f, rotationRight ? 0f : 180f, 0f);
            }
        }

        private void HandleRun(bool run) {
            speed *= run ? 2 : 0.5f;
        }

        private void HandleJump() {
            const float multiplier = 3f;
            if (contactChecker.IsGrounded)
            {
                _rb.linearVelocity = Vector2.zero;
                _rb.AddForceY(jumpForce * multiplier, ForceMode2D.Impulse);
            }
        }

        private void HandleAttackInput(int btn, bool pressed) {
            HoldingItem?.Use(new ItemUsingContext {
                Input = btn,
                Pressed = pressed,
                User = _owner
            });
        }

        private void HandleSkill1Input() {
            hp.UpdateHp(explosion);
            vfxBoom.transform.position = transform.position;
            impulseSource.GenerateImpulseWithForce(1f);
            //for (var i = 0; i < testDam.Length; i++) testDam[i]?.largePush();
            vfx.Play();
        }

        private void HandleSkill2Input() {
            if (isDashing) return;
            StartCoroutine(Dash());
            dashVfx?.Play();
        }

        private IEnumerator Dash() {
            isDashing = true;
            ani.OnDashAni(true);
            var dashDirection = rotationRight ? 2f : -2f;
            _rb.linearVelocity = new Vector2(dashDirection * dashForce, 0f);

            yield return new WaitForSeconds(dashDuration);
            ani.OnDashAni(false);
            isDashing = false;
        }

        private void HandleSkill3Input() {
            if (IsFFF) return;
            StartCoroutine(FFFActive());
        }

        private IEnumerator FFFActive() {
            IsFFF = true;
            yield return new WaitForSeconds(skill3ActiveTime);
            IsFFF = false;
        }

        private void OnDestroy() {
            _input.OnMoveInput -= HandleMoveInput;
            _input.OnRunInput -= HandleRun;
            _input.OnJumpInput -= HandleJump;
            _input.OnAttackInput -= HandleAttackInput;
            _input.OnSkill1Input -= HandleSkill1Input;
            _input.OnSkill2Input -= HandleSkill2Input;
            _input.OnGuardInput -= HandleGuard;
            _input.OnSkill3Input -= HandleSkill3Input;
        }
    }
}
