using UnityEngine;

namespace _03_Code.Exp {
    public class Exp : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float range = 10f;


        private Vector2 _moveDir;

        private void FixedUpdate() {
            var circle = Physics2D.OverlapCircle(rb.position, range, LayerMask.GetMask("Player"));
            if (!circle) return;
            _moveDir = circle.transform.position - transform.position;
            _moveDir.Normalize();
            rb.linearVelocity = _moveDir * moveSpeed;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(rb.position, range);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Destroy(gameObject);
        }
    }
}