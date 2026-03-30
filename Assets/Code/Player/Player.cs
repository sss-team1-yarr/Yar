using UnityEngine;

namespace Code.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Debug.Log("update");
            rb.linearVelocity = rb.velocity;
        }
    }
}