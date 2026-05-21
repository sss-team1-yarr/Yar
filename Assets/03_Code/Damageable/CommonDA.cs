using _03_Code.Enemy.Common;
using _03_Code.Enemy.Common.Component;
using _03_Code.Enemy.Interface;
using UnityEngine;

namespace _03_Code.Damageable {
    public class CommonDA : MonoBehaviour, IDamageable {
        [SerializeField] private ParticleSystem chargingVfx;
        [SerializeField] private ParticleSystem normalVfx;
        [SerializeField] private Monster mob;
        [SerializeField] private EnemyMove em;

        public DamageResult ApplyDamage(int damageAmount, bool isCharge) {
            if (mob.IsDead) return new DamageResult();
            
            if (isCharge)
                chargingVfx.Play();
            else
                normalVfx.Play();
            
            em.KnockBack(damageAmount, isCharge);
            return new DamageResult {
                Hit = true
            };
        }
    }
}