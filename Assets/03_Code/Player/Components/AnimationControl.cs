using UnityEngine;

namespace _03_Code.Player.Components {
    public class AnimationControl : MonoBehaviour {
        [SerializeField] private PlayerRenderer playerRenderer;
        
        private static readonly int XVelocityHash = Animator.StringToHash("XVelocity");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

        public void OnJumpAni(bool isGrounded) {
            playerRenderer.SetBoolValue(IsGroundedHash, isGrounded);
        }

        public void OnMoveAni(float move) {
            playerRenderer.SetFloatValue(XVelocityHash, move);
        }
    }
}