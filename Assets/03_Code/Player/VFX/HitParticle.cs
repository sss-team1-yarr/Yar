using UnityEngine;

namespace _03_Code.Player.VFX {
    public class HitParticle : MonoBehaviour {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float offsetDistance = 0.7f;

        public void PlayHitEffect() {
            var nearEnemyTrm = FindNearestEnemy();
            if (!nearEnemyTrm) return;

            Vector2 dir = (nearEnemyTrm.position - transform.parent.position).normalized;
            transform.localPosition = dir * offsetDistance;
        }

        private Transform FindNearestEnemy() {
            var enemies = Physics2D.OverlapCircleAll(transform.position, 10f, enemyLayer);

            if (enemies.Length == 0) return null;

            Transform nearest = null;
            var minDistance = Mathf.Infinity;

            foreach (var enemy in enemies) {
                var distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance) {
                    minDistance = distance;
                    nearest = enemy.transform;
                }
            }

            return nearest;
        }
    }
}