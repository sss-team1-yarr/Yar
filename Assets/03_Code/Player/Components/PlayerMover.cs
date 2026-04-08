using UnityEngine;

namespace _03_Code.Player.Components
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private Rigidbody2D rb;

        private float _moveInput;
        public Vector2 Velocity => rb.linearVelocity;

        public void SetMoveInput(float value)
        {
            _moveInput = value;
        }
        
        public void Jump(float multiplier = 3f)
        {
            rb.AddForceY(jumpForce * multiplier, ForceMode2D.Impulse);
        }
    
        private void FixedUpdate() {
            rb.linearVelocityX = _moveInput * speed;
        }
    }
}