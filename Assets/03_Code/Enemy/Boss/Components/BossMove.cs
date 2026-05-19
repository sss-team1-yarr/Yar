using UnityEngine;

namespace _03_Code.Enemy.Boss.Components {
    public class BossMove : MonoBehaviour {
        [Header("Components")] [SerializeField]
        private Boss monster;

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
            if (_isKnockedBack || monster.Death) return;

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
    }
}