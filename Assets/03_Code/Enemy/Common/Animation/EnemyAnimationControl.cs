using UnityEngine;

namespace _03_Code.Enemy.Common.Animation {
    public class EnemyAnimationControl : MonoBehaviour {
        private static readonly int IsDeadHash = Animator.StringToHash("IsDead");
        [SerializeField] private EnemyRenderer enemyRenderer;

        public void OnDeadAni(bool isDead) {
            enemyRenderer.SetBoolValue(IsDeadHash, isDead);
        }
    }
}