using System.Collections;
using _03_Code.Enemy.Common.Animation;
using _03_Code.SO;
using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class Monster : MonoBehaviour {
        [Header("EnemySO")] 
        [SerializeField] private EnemySO enemySO;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D coll;
        

        [Header("Components")]
        [SerializeField] private ShowHp sHp;
        [SerializeField] private EnemyAnimationControl enemyAnim;
        
        public float ApproachForce { get; private set; }
        public float ApproachTime { get; private set; }
        public int ApproachDamage { get; private set; }
        public float Speed { get; private set; }
        public float DetectRange{ get; private set; }
        public float KnockBackForce{ get; private set; }
        public float KnockBackTime{ get; private set; }
        public bool IsDead { get; private set; } = false;
        
        private float _scale;
        private int _enemyHp;
        private int _dropExp;
        
        private void Awake()
        {
            _scale = enemySO.scale;
            Speed = enemySO.speed;
            _enemyHp = enemySO.health;
            ApproachForce = enemySO.approachForce;
            ApproachTime = enemySO.approachTime;
            ApproachDamage = enemySO.approachDamage;
            DetectRange = enemySO.detectRange;
            KnockBackForce = enemySO.knockBackForce;
            KnockBackTime = enemySO.knockBackTime;
            _dropExp = enemySO.dropExp;
        }
        
        private void Start()
        {
            transform.localScale = Vector3.one * _scale;
            UpdateHP(_enemyHp);
        }

        private void Update() {
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
            GameManager.Instance.expDropManager.DropExp(gameObject, _dropExp);
            gameObject.SetActive(false);
        }

        public void GetDamage(int damage) {
            _enemyHp -=  damage;
            UpdateHP(_enemyHp);
        }

        public void UpdateHP(int hp) {
            sHp.UpdateHp(hp);
            if (_enemyHp <= 0) {
                _enemyHp = 0;
                IsDead = true;
            }
        }
    }
}