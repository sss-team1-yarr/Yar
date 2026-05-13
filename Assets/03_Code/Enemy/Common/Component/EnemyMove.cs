using System;
using System.Collections;
using _03_Code.Enemy.Common.Animation;
using _03_Code.SO;
using UnityEngine;

namespace _03_Code.Enemy.Common.Component {
    public class EnemyMove : MonoBehaviour
    {
        [Header("EnemySO")] 
        [SerializeField] private EnemySO enemySO;
        
        [Header("Components")]
        [SerializeField] private Collider2D coll;
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform player;
        [SerializeField] private ShowHp sHp;
        [SerializeField] private EnemyAnimationControl enemyAnim;
        
        [Header("Settings")]
        [SerializeField] private float detectRange = 5f;
        [SerializeField] private float knockBackForce = 40f;
        [SerializeField] private float knockBackTime = 0.1f;

        public float ApproachForce { get; private set; }
        public float ApproachTime { get; private set; }
        public int ApproachDamage { get; private set; }
        
        private bool _isKnockedBack = false;
        private float _speed;
        private int _enemyHp;

        public bool IsDead { get; private set; } = false;
        
        public float Direction { get; private set; }

        private void Reset()
        {
            coll = GetComponent<Collider2D>();
            sr = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player").transform; //태그로 하니깐 자꾸 태그를 못찾아서 오류남
            sHp = GetComponentInChildren<ShowHp>();
            enemyAnim = GetComponent<EnemyAnimationControl>();
        }

        // input EnemySO Value
        private void Awake()
        {
            _speed = enemySO.speed;
            _enemyHp = enemySO.health;
            ApproachForce = enemySO.approachForce;
            ApproachTime = enemySO.approachTime;
            ApproachDamage = enemySO.approachDamage;
        }
        
        private void Start()
        {
            sHp.UpdateHp(_enemyHp);
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

            rb.linearVelocity = new Vector2(Direction * _speed, rb.linearVelocity.y);
            sr.flipX = Direction < 0f;
        }
    
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(rb.position, detectRange);
        }
        
        public void KnockBack(int damageAmount)
        {
            if (_isKnockedBack || IsDead) return;

            if (_enemyHp - damageAmount > 0f)
                _enemyHp -= damageAmount;
            else
            {
                _enemyHp = 0;
                IsDead = true;
            }
            
            sHp.UpdateHp(_enemyHp);
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
            GameManager.Instance.expDropManager.DropExp(gameObject);
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

