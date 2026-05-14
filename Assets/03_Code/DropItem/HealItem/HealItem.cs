using System;
using UnityEngine;

namespace _03_Code.DropItem.HealItem {
    public class HealItem : MonoBehaviour {
        [SerializeField] private float range = 1f;

        private void FixedUpdate() {
            var circle = Physics2D.OverlapCircle(transform.position, range, LayerMask.GetMask("Player"));
            if (circle) {
                OnUsed();
            }
        }
        
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, range);
        }

        private void OnUsed() {
            GameManager.Instance.hpManager.UpdateHp(-5);
            Destroy(gameObject);
        }
    }
}