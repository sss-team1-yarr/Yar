using _03_Code.Player.Components;
using UnityEngine;

namespace _03_Code.Enemy.Common.Animation
{
    public class EnemyAnimationControl : MonoBehaviour
    {
        [SerializeField] private EnemyRenderer enemyRenderer;
        
        private static readonly int IsDeadHash = Animator.StringToHash("IsDead");

        public void OnDeadAni(bool isDead) {
            enemyRenderer.SetBoolValue(IsDeadHash, isDead);
        }
    }
}