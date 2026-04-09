using _03_Code.Items;
using _03_Code.Player.Components;
using _3_Code.Player.Components;
using UnityEngine;
using UnityEngine.XR;

namespace _03_Code.Player {
    public class Player : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputReceiver input;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private PlayerRenderer playerRenderer;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private HpManager hp;
        
        
        public IItem HoldingItem { get; private set; }
        private static readonly int XVelocityHash = Animator.StringToHash("XVelocity");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");


        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            input.OnJumpInput += HandleJump;
            input.OnMoveInput += HandleMoveInput;
            input.OnAttackInput += HandleAttackInput;
            input.OnSkill1Input += HandleSkill1Input;
        }

        private void Update() {
            playerRenderer.SetBoolValue(IsGroundedHash, contactChecker.IsGrounded);
            if(hp.hp <= 0) HandlePlayerDeath();
        }

        private void HandleMoveInput(float obj) {
            playerMover.SetMoveInput(obj);
            playerRenderer.SetFloatValue(XVelocityHash, Mathf.Abs(obj));
            if (!Mathf.Approximately(obj, 0f)) playerRenderer.SetFlip(obj > 0);
        }

        private void HandleJump() {
            if (contactChecker.IsGrounded)
                playerMover.Jump();
        }
        private void HandleAttackInput(int btn, bool pressed) {
            HoldingItem?.Use(new ItemUsingContext {
                Input = btn,
                Pressed = pressed,
                User = this
            });
        }
        private void HandleSkill1Input() {
            var dam = 1 - hp.hp;
            hp.UpdateHp(dam);
            vfx.Play();
        }

        private void HandlePlayerDeath() {
            Destroy(gameObject);
            Time.timeScale = 0;
        }

        private void OnDestroy() {
            input.OnJumpInput -= HandleJump;
            input.OnMoveInput -= HandleMoveInput;
            input.OnSkill1Input -= HandleSkill1Input;
        }
    }
}