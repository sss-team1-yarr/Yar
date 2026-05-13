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
        
        public float ApproachForce { get; private set; }
        public float ApproachTime { get; private set; }
        public int ApproachDamage { get; private set; }

        private float _scale;
        private float _speed;
        private int _enemyHp;
        private float _detectRange;
        private float _knockBackForce;
        private float _knockBackTime;

        private bool _isKnockedBack = false;
        
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
            _scale = enemySO.scale;
            _speed = enemySO.speed;
            _enemyHp = enemySO.health;
            ApproachForce = enemySO.approachForce;
            ApproachTime = enemySO.approachTime;
            ApproachDamage = enemySO.approachDamage;
            _detectRange = enemySO.detectRange;
            _knockBackForce = enemySO.knockBackForce;
            _knockBackTime = enemySO.knockBackTime;
        }
        
        private void Start()
        {
            transform.localScale = Vector3.one * _scale;
            sHp.UpdateHp(_enemyHp);
        }

        private void FixedUpdate()
        {
            if (_isKnockedBack || IsDead) return;
        
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance > _detectRange)
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                return;
            }

            Direction = player.position.x > transform.position.x ? 1f : -1f;

            rb.linearVelocity = new Vector2(Direction * _speed, rb.linearVelocity.y);
            sr.flipX = Direction < 0f;
        }
    
        private void OnDrawGizmos() {
            if (_detectRange != 0)
                Gizmos.DrawWireSphere(rb.position, _detectRange);
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
            rb.AddForce(new Vector2(_knockBackForce * -Direction, 0f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(_knockBackTime);
            _isKnockedBack = false;
        }
    }
}

