using UnityEngine;

namespace _03_Code.Player.Main
{
    public class PlayerMove : MonoBehaviour
    {
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponentInParent<Rigidbody2D>();
        }
        
        private void FixedUpdate() {
            if (GameManager.Instance.playerSystem.IsDashing || GameManager.Instance.playerHit.IsApproach) return;
            _rb.linearVelocityX = GameManager.Instance.playerSystem.MoveInput * GameManager.Instance.playerSystem.Speed;
        }
    }
}
