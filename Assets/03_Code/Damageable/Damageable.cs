using _03_Code.Enemy.Common.FlyEnemy;
using _03_Code.Enemy.Interface;
using UnityEngine;

namespace _03_Code.Damageable {
    
    //이건 사용 안함
    public class Damageable : MonoBehaviour, IDamageable {
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private FlyEnemy enemy;

        public DamageResult ApplyDamage(int damageAmount, bool isCharge) {
            vfx.Play();
            StartCoroutine(enemy.Hit());
            return new DamageResult {
                Hit = true
            };
        }
    }
}