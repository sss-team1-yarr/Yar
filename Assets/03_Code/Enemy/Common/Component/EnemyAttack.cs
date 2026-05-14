using UnityEngine;

namespace _03_Code.Enemy.Common.Component
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Monster em; 
        
        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
            em = GetComponent<Monster>();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 direction = - collision.contacts[0].normal;
                StartCoroutine(GameManager.Instance.playerHit.Approach(direction, em));
            }
        }
    }
}
