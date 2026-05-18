using _03_Code.Enemy.Boss;
using _03_Code.Enemy.Common.Component;
using _03_Code.Enemy.Interface;
using UnityEngine;

namespace _03_Code.Damageable {
    public class BossDA : MonoBehaviour, IDamageable {
        [SerializeField] private ParticleSystem vfx; 
        [SerializeField] private Boss mob;
        [SerializeField] private EnemyMove em;

        public DamageResult ApplyDamage(int damageAmount) {
            if (mob.Death) return new DamageResult();

            vfx.Play();
            em.KnockBack(damageAmount);
            return new DamageResult {
                Hit = true
            };
        }
    }
}