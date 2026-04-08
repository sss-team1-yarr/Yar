using UnityEngine;

namespace _03_Code.Player.Components
{
    public class ContactChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private Vector2 size;
        [SerializeField] private Vector2 offset;
        [SerializeField] private Rigidbody2D rb;

        public bool IsGrounded { get; private set; }

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapBox(rb.position + offset, size, 0f, targetLayer);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position+(Vector3)offset, size);
        }
    }
}