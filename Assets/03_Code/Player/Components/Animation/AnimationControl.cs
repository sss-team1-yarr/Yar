using UnityEngine;

namespace _03_Code.Player.Components {
    public class AnimationControl : MonoBehaviour {
        private static readonly int XVelocityHash = Animator.StringToHash("XVelocity");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        private static readonly int IsDashHash = Animator.StringToHash("IsDash");
        private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
        [SerializeField] private PlayerRenderer playerRenderer;

        public void OnJumpAni(bool isGrounded) {
            playerRenderer.SetBoolValue(IsGroundedHash, isGrounded);
        }

        public void OnMoveAni(float move) {
            playerRenderer.SetFloatValue(XVelocityHash, move);
        }

        public void OnDashAni(bool isDash) {
            playerRenderer.SetBoolValue(IsDashHash, isDash);
        }

        public void OnDeadAni(bool isDead) {
            playerRenderer.SetBoolValue(IsDeadHash, isDead);
        }
    }
}