using System;
using UnityEngine;

namespace _03_Code.Player.VFX
{
    public class HitParticle : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float offsetDistance = 0.7f;
        
        public void PlayHitEffect()
        {
            Transform nearEnemyTrm = FindNearestEnemy();
            if (!nearEnemyTrm) return;
    
            Vector2 dir = (nearEnemyTrm.position - transform.parent.position).normalized;
            transform.localPosition = dir * offsetDistance;
            
        }
        
        private Transform FindNearestEnemy()
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);

            if (enemies.Length == 0) return null;

            Transform nearest = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider2D enemy in enemies)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy.transform;
                }
            }

            return nearest;
        }
    }
}
