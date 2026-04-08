using _03_Code.Player.Components;
using Code.Player.Components;
using UnityEngine;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private InputReceiver input;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private ContactChecker contactChecker;
        [SerializeField] private PlayerRenderer playerRenderer;

        private static readonly int XVelocityHash = Animator.StringToHash("XVelocity");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        
        

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            input.OnJumpInput += HandleJump;
            input.OnMoveInput += HandleMoveInput;
        }
        
        private void Update()
        {
            playerRenderer.SetBoolValue(IsGroundedHash, contactChecker.IsGrounded);
        }

        private void HandleMoveInput(float obj)
        {
            playerMover.SetMoveInput(obj);
            playerRenderer.SetFloatValue(XVelocityHash, Mathf.Abs(obj));
            if (!Mathf.Approximately(obj, 0f)) playerRenderer.SetFlip(obj > 0);
        }

        private void HandleJump()
        {
            if(contactChecker.IsGrounded)
                playerMover.Jump();
        }


        private void OnDestroy()
        {
            input.OnJumpInput -= HandleJump;
            input.OnMoveInput -= HandleMoveInput;
        }
    }
}