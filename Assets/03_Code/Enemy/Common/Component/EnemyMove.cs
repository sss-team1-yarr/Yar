using System.Collections;
using _03_Code.VFX;
using UnityEngine;

namespace _03_Code.Enemy.Common.Component {
    public class EnemyMove : MonoBehaviour {
        [Header("Components")] [SerializeField]
        private Monster monster;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer sr;
        private float _detectRange;
        private bool _isKnockedBack;

        private float _speed;

        private float Direction = 1f;

        private void Start() {
            _speed = monster.Speed;
            _detectRange = monster.DetectRange;
        }

        private void FixedUpdate() {
            if (_isKnockedBack || monster.IsDead) return;

            var target = Physics2D.OverlapCircle(rb.position, _detectRange, LayerMask.GetMask("Player"));
            if (!target) {
                rb.linearVelocityX = 0f;
                return;
            }

            Direction = target.transform.position.x > rb.position.x ? 1 : -1;
            rb.linearVelocityX = Direction * _speed;
            sr.flipX = Direction < 0f;
        }

        private void OnDrawGizmos() {
            if (_detectRange != 0)
                Gizmos.DrawWireSphere(rb.position, _detectRange);
        }

        public void KnockBack(int damageAmount, bool isCharge) {
            if (_isKnockedBack || monster.IsDead) return;

            monster.GetDamage(damageAmount);
            
            if (isCharge)
                StartCoroutine(KnockBackRoutine());
        }

        private IEnumerator KnockBackRoutine() {
            if (monster.IsDead) yield break;

            _isKnockedBack = true;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(monster.KnockBackForce * -Direction, 0f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(monster.KnockBackTime);
            _isKnockedBack = false;
        }
    }
}