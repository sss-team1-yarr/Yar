using System;
using System.Collections;
using _03_Code.Enemy.Common.Animation;
using UnityEngine;
using UnityEngine.Serialization;

namespace _03_Code.Enemy.Common.Component {
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private Collider2D coll;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform player;
        [SerializeField] private ShowHp sHp;
        [SerializeField] private EnemyAnimationControl enemyAnim;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float detectRange = 5f;
        [SerializeField] private float knockBackForce = 40f;
        [SerializeField] private float knockBackTime = 0.1f;
        [SerializeField] private int enemyHp = 100;

        private bool _isKnockedBack = false;

        public bool IsDead { get; private set; } = false;
        public float Direction { get; private set; }
        
        private void Reset()
        {
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player").transform; //태그로 하니깐 자꾸 태그를 못찾아서 오류남
            sHp = GetComponentInChildren<ShowHp>();
            enemyAnim = GetComponent<EnemyAnimationControl>();
        }

        private void FixedUpdate()
        {
            if (_isKnockedBack || IsDead) return;
        
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > detectRange)
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                return;
            }

            Direction = player.position.x > transform.position.x ? 1f : -1f;

            rb.linearVelocity = new Vector2(Direction * speed, rb.linearVelocity.y);
            sr.flipX = Direction < 0f;
        }
    
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(rb.position, detectRange);
        }
        
        public void KnockBack(int damageAmount)
        {
            if (_isKnockedBack || IsDead) return;

            if (enemyHp - damageAmount > 0f)
                enemyHp -= damageAmount;
            else
            {
                enemyHp = 0;
                IsDead = true;
            }
            
            sHp.UpdateHp(enemyHp);
            StartCoroutine(KnockBackRoutine());
            if (IsDead)
                StartCoroutine(Dead());
        }

        private IEnumerator Dead()
        {
            coll.isTrigger = true;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            
            enemyAnim.OnDeadAni(true);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }

        private IEnumerator KnockBackRoutine() {
            if (IsDead) yield break;
            
            _isKnockedBack = true;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(knockBackForce * -Direction, 0f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(knockBackTime);
            _isKnockedBack = false;
        }
    }
}

