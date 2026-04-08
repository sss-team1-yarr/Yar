using _03_Code.Player.Components;
using UnityEngine;

namespace _03_Code.Player {
    public class Player : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputReceiver input;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private PlayerRenderer playerRenderer;
        [SerializeField] private ParticleSystem vfx;
        

        private static readonly int XVelocityHash = Animator.StringToHash("XVelocity");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

        private bool _isJumpKeyPressed;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
            input.OnJumpInput += HandleJump;
            input.OnMoveInput += HandleMoveInput;
            input.OnSkill1Input += HandleSkill1Input;
            input.OnAttackInput += HandleAttackInput;
        }

        private void Update() {
            playerRenderer.SetBoolValue(IsGroundedHash, contactChecker.IsGrounded);
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
        
        private void HandleAttackInput() {
            
        }

        private void HandleSkill1Input() {
            vfx.Play();
            Destroy(gameObject);
        }

        private void OnDestroy() {
            input.OnJumpInput -= HandleJump;
            input.OnMoveInput -= HandleMoveInput;
            input.OnSkill1Input -= HandleSkill1Input;
        }
    }
}