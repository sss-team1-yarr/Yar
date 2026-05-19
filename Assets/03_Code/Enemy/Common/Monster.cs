using System.Collections;
using _03_Code.Enemy.Common.Animation;
using _03_Code.SO;
using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class Monster : MonoBehaviour {
        [Header("EnemySO")] [SerializeField] private EnemySO enemySO;

        [Header("Components")] [SerializeField]
        private Rigidbody2D rb;

        [SerializeField] private Collider2D coll;
        [SerializeField] private ShowHp sHp;
        [SerializeField] private EnemyAnimationControl enemyAnim;
        [SerializeField] private Animator anim;

        private RuntimeAnimatorController _animController;

        private int _dropExp;
        private int _health;

        private float _scale;

        public float ApproachForce { get; private set; }
        public float ApproachTime { get; private set; }
        public int ApproachDamage { get; private set; }
        public float Speed { get; private set; }
        public float DetectRange { get; private set; }
        public float KnockBackForce { get; private set; }
        public float KnockBackTime { get; private set; }
        public bool IsDead { get; private set; }
        public int MaxHP { get; private set; }

        private void Awake() {
            _scale = enemySO.scale;
            Speed = enemySO.speed;
            MaxHP = enemySO.health;
            ApproachForce = enemySO.approachForce;
            ApproachTime = enemySO.approachTime;
            ApproachDamage = enemySO.approachDamage;
            DetectRange = enemySO.detectRange;
            KnockBackForce = enemySO.knockBackForce;
            KnockBackTime = enemySO.knockBackTime;
            _dropExp = enemySO.dropExp;
            _animController = enemySO.animController;
        }

        private void Start() {
            _health = MaxHP;
            transform.localScale = Vector3.one * _scale;
            UpdateHP(_health);
            anim.runtimeAnimatorController = _animController;
        }

        private void Update() {
            if (IsDead)
                StartCoroutine(Dead());
        }

        private IEnumerator Dead() {
            coll.isTrigger = true;
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;

            enemyAnim.OnDeadAni(true);
            yield return new WaitForSeconds(2f);
            GameManager.Instance.dropManager.DropItem(gameObject, _dropExp);
            gameObject.SetActive(false);
        }

        public void GetDamage(int damage) {
            _health -= damage;
            UpdateHP(_health);
        }

        public void UpdateHP(int hp) {
            if (_health <= 0) {
                _health = 0;
                IsDead = true;
            }

            sHp.UpdateHp(hp);
        }
    }
}