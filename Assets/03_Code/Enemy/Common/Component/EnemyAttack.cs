using _03_Code.Manager;
using UnityEngine;

namespace _03_Code.Enemy.Common.Component {
    public class EnemyAttack : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Monster em;


        private void Reset() {
            rb = GetComponent<Rigidbody2D>();
            em = GetComponent<Monster>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (em.IsDead) return;
            
            if (collision.gameObject.CompareTag("Player")) {
                var direction = (collision.transform.position - transform.position).normalized;
                StartCoroutine(GameManager.Instance.playerHit.Approach(direction, em));
            }
        }
    }
}