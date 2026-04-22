using _03_Code.Items;
using _03_Code.Items.Weapons;
using _03_Code.Player.Interface;
using UnityEngine;

namespace _03_Code.Player.Components {
    public class PlayerMove : MonoBehaviour, IPlayerModule {
        [SerializeField] private int explosion;
        [SerializeField] private float jumpForce;
        [SerializeField] private AnimationControl ani;
        [SerializeField] private HpManager hp;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private GameObject vfxBoom;
        [SerializeField] private ContactChecker contactChecker;
        [field: SerializeField] public Weapon HoldingItem { get; private set; }
        [SerializeField] private TestDamageable[] testDam;

        private Player _owner;
        private Rigidbody2D _rb;
        private InputReceiver _input;

        public void Initialize(Player owner) {
            _owner = owner;
            _rb = owner.GetComponent<Rigidbody2D>();
            _input = _owner.GetModule<InputReceiver>();
            _input.OnJumpInput += HandleJump;
            _input.OnRunInput += HandleRun;
            _input.OnMoveInput += HandleMoveInput;
            _input.OnAttackInput += HandleAttackInput;
            _input.OnSkill1Input += HandleSkill1Input;
        }

        private void Awake() {
            HoldingItem?.HoldItem(new ItemUsingContext { User = _owner, Input = 0, Pressed = true });
        }

        private float Speed { get; set; } = 8f;
        private float _moveInput;

        private void HandleMoveInput(float value) {
            _moveInput = value;
            ani.OnMoveAni(Mathf.Abs(value));
            if (!Mathf.Approximately(value, 0f))
                _owner.transform.rotation = Quaternion.Euler(0f, value > 0f ? 0f : 180f, 0f);
        }

        private void HandleRun(bool run) {
            Speed *= run ? 2 : 0.5f;
        }

        private void HandleJump() {
            const float multiplier = 3f;
            if (contactChecker.IsGrounded)
                _rb.AddForceY(jumpForce * multiplier, ForceMode2D.Impulse);
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
            for (int i = 0; i < testDam.Length; i++)
            {
                testDam[i].largePush();
            }
            vfx.Play();
        }

        private void FixedUpdate() {
            _rb.linearVelocityX = _moveInput * Speed;
        }

        private void OnDestroy() {
            _input.OnMoveInput -= HandleMoveInput;
            _input.OnRunInput -= HandleRun;
            _input.OnJumpInput -= HandleJump;
            _input.OnAttackInput -= HandleAttackInput;
            _input.OnSkill1Input -= HandleSkill1Input;
        }
    }
}