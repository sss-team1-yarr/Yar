using UnityEngine;

namespace _03_Code.Enemy.Boss.Components {
    public class BossAttack : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Player")) {
                StartCoroutine(GameManager.Instance.playerHit.Approach());
            }
        }
    }
}