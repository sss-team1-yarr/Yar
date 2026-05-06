using System.Collections;
using UnityEngine;

namespace _03_Code.Player.Components
{
    public class PlayerHit : MonoBehaviour
    { 
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float approachForce = 10f;
        [SerializeField] private int approachDamage = 10;
        [SerializeField] private float approachTime = 1f;
        
        public bool IsApproach { get; private set; }= false;

        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        public IEnumerator Approach(float direction)
        {
            if (IsApproach) yield break;
            
            IsApproach = true;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(direction, 1f) * approachForce, ForceMode2D.Impulse);
            GameManager.Instance.hpManager.UpdateHp(approachDamage);
            yield return new WaitForSeconds(approachTime);
            IsApproach = false;
        }
    }
}
