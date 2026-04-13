using System;
using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class IsNearbyMe : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float mobDistance = 3f;
        
        public Vector2 MoveDir { get; private set; }
        
        private void FixedUpdate() {
            var circle = Physics2D.OverlapCircle(rb.position, mobDistance, LayerMask.GetMask("Player"));
            if (!circle) {
                rb.linearVelocity = Vector2.zero;
                return;
            }
            MoveDir = circle.transform.position - transform.position;
            MoveDir.Normalize();
            rb.linearVelocity=MoveDir * speed;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(rb.position, mobDistance);
        }
    }
}
