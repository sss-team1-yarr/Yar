using System.Collections;
using _03_Code.Enemy.Common;
using _03_Code.Enemy.Common.Component;
using UnityEngine;

namespace _03_Code.Player.Components
{
    public class PlayerHit : MonoBehaviour
    { 
        [SerializeField] private Rigidbody2D rb;
        
        public bool IsApproach { get; private set; }= false;

        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();//?
        }
        
        public IEnumerator Approach(Vector2 direction, Monster em)
        {
            if (IsApproach) yield break;
            
            IsApproach = true;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * em.ApproachForce, ForceMode2D.Impulse);
            GameManager.Instance.hpManager.UpdateHp(em.ApproachDamage);
            yield return new WaitForSeconds(em.ApproachTime);
            IsApproach = false;
        }
    }
}
