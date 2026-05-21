using _03_Code.Enemy.Boss;
using _03_Code.Enemy.Boss.Components;
using _03_Code.Enemy.Interface;
using UnityEngine;

namespace _03_Code.Damageable {
    public class BossDA : MonoBehaviour, IDamageable {
        [SerializeField] private ParticleSystem chargingVfx;
        [SerializeField] private ParticleSystem normalVfx;
        [SerializeField] private Boss mob;
        [SerializeField] private BossHpManager hpManager;

        public DamageResult ApplyDamage(int damageAmount, bool isCharge) {
            if (mob.Death) return new DamageResult();
            
            if (isCharge)
                chargingVfx.Play();
            else
                normalVfx.Play();
            
            hpManager.Damage(damageAmount);
            return new DamageResult {
                Hit = true
            };
        }
    }
}