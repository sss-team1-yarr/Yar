using UnityEngine;

namespace _03_Code.Enemy.Common.FlyEnemy {
    public class IsNearbyMe : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float mobDistance = 3f;

        public Vector2 MoveDir { get; private set; }

        private void Reset() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            var circle = Physics2D.OverlapCircle(rb.position, mobDistance, LayerMask.GetMask("Player"));
            if (!circle) {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            MoveDir = circle.transform.position - transform.position;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(rb.position, mobDistance);
        }
    }
}