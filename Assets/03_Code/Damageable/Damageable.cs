using _03_Code.Enemy.Common;
using _03_Code.Enemy.Common.FlyEnemy;
using _03_Code.Enemy.Interface;
using UnityEngine;

namespace _03_Code.Damageable {
    public class Damageable : MonoBehaviour, IDamageable {
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private FlyEnemy enemy;

        public DamageResult ApplyDamage(DamageInfo info) {
            vfx.Play();
            StartCoroutine(enemy.Hit(info.KnockbackForce));
            return new DamageResult {
                Hit = true
            };
        }
    }
}