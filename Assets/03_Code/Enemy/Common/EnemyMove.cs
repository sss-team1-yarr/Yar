using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform player;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float detectRange = 5f;
        [SerializeField] private float knockBackForce = 20f;
        [SerializeField] private float knockBackTime = 0.1f;

        private float _direction;
        private bool _isKnockedBack = false;
    
        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            if (_isKnockedBack) return;
        
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > detectRange)
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                return;
            }

            _direction = player.position.x > transform.position.x ? 1f : -1f;

            rb.linearVelocity = new Vector2(_direction * speed, rb.linearVelocity.y);
        }
    
        public void KnockBack()
        {
            if (_isKnockedBack) return;
            StartCoroutine(KnockBackRoutine());
        }

        private IEnumerator KnockBackRoutine() {
            _isKnockedBack = true;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(knockBackForce * -_direction, 0f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(knockBackTime);
            _isKnockedBack = false;
        }
    }
}

