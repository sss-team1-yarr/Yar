using System.Collections;
using _03_Code.Manager;
using _03_Code.Player.Components;
using UnityEngine;

namespace _03_Code.Enemy.Common.FlyEnemy {
    public class FlyEnemy : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private int damage;
        [SerializeField] private HpManager hp;
        [SerializeField] private PlayerRenderer playerRenderer;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float enemyKnockBackForce = 5f;
        [SerializeField] private float hitDuration = 0.1f;
        private bool _isHit;

        private IsNearbyMe _scan;

        private void Awake() {
            _scan = GetComponent<IsNearbyMe>();
        }

        private void Update() {
            transform.rotation = Quaternion.Euler(0f, _scan.MoveDir.x > 0f ? 0f : 180f, 0f);
        }

        private void FixedUpdate() {
            if (!_isHit)
                rb.linearVelocity = _scan.MoveDir.normalized * speed;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            hp.Damage(damage);
        }

        public IEnumerator Hit() {
            if (_isHit) yield break;
            _isHit = true;
            var dir = transform.position.x > GameManager.Instance.playerControl.transform.position.x ? 1f : -1f;

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(dir * enemyKnockBackForce, enemyKnockBackForce),
                ForceMode2D.Impulse);

            yield return new WaitForSeconds(hitDuration);
            _isHit = false;
        }
    }
}