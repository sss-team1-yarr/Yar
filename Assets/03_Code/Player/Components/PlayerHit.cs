using System.Collections;
using UnityEngine;

namespace _03_Code.Player.Components
{
    public class PlayerHit : MonoBehaviour
    { 
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float approachForce;
        
        public bool IsApproach { get; private set; }= false;

        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        public IEnumerator Approach(float direction)
        {
            if (IsApproach) yield break;
            
            IsApproach = true;
            Debug.Log("밀려났다!");
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(direction, 1f) * approachForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f);
            IsApproach = false;
        }
    }
}
