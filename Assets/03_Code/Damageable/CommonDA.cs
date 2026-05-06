using _03_Code.Enemy.Common;
using _03_Code.Enemy.Interface;
using UnityEngine;

namespace _03_Code.Damageable {
    public class CommonDA : MonoBehaviour, IDamageable
    {
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private EnemyMove em;

        public DamageResult ApplyDamage(DamageInfo info)
        {
            if (em.IsDead) return new DamageResult();
            
            vfx.Play();
            em.KnockBack(info.DamageAmount);
            return new DamageResult {
                Hit = true
            };
        }
    }
}
