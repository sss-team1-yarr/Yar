using System.Collections;
using _03_Code.Player.Main;
using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class IsNearbyMe : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private PlayerMove pm;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float mobDistance = 3f;
        [SerializeField] private float hitDuration = 0.5f;
        [SerializeField] private float enemyKnockBackForce = 5f;
        
        public Vector2 MoveDir { get; private set; }
        
        private bool _isHit;

        private void Reset() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            if (_isHit) return;
            
            var circle = Physics2D.OverlapCircle(rb.position, mobDistance, LayerMask.GetMask("Player"));
            if (!circle) {
                rb.linearVelocity = Vector2.zero;
                return;
            }
            
            MoveDir = circle.transform.position - transform.position;
            MoveDir.Normalize();
            rb.linearVelocity = MoveDir * speed;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(rb.position, mobDistance);
        }

        public IEnumerator Hit(float knockbackForce) {
            if (_isHit) yield break; 
            _isHit = true;
            float dir = transform.position.x > pm.transform.position.x ? 1f : -1f;
            
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(knockbackForce * dir * enemyKnockBackForce, knockbackForce * enemyKnockBackForce), 
                ForceMode2D.Impulse);
            
            yield return new WaitForSeconds(hitDuration);
            _isHit = false; 
        }
    }
}