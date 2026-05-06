using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform player;
        [SerializeField] private ShowHp sHp;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float detectRange = 5f;
        [SerializeField] private float knockBackForce = 40f;
        [SerializeField] private float knockBackTime = 0.1f;
        [SerializeField] private int enemyHp = 100;

        private float _direction;
        private bool _isKnockedBack = false;
    
        private void Reset()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.FindWithTag("Player").transform;
            sHp = GetComponentInChildren<ShowHp>();
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
            sr.flipX = _direction < 0f;
        }
    
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(rb.position, detectRange);
        }
        
        public void KnockBack(int damageAmount)
        {
            if (_isKnockedBack) return;

            if (enemyHp - damageAmount > 0f)
                enemyHp -= damageAmount;
            else
                enemyHp = 0;
            
            sHp.UpdateHp(enemyHp);
            StartCoroutine(KnockBackRoutine());
            gameObject.SetActive(false);
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

