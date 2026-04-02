using UnityEngine;

namespace Code.Player.Components
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float jumpForce;

        private float _moveInput; 
        
        public void Jump(float multiplier = 3f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed * multiplier;
        }

        public void SetMoveInput(float value)
        {
            _moveInput = value;
        }
    
        private void FixedUpdate() {
            rb.linearVelocityX = _moveInput * speed;
        }
    }
}