using _03_Code.Interface;
using UnityEngine;

namespace _03_Code {
    public class Damageable : MonoBehaviour, IDamageable {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem vfx;

        public DamageResult ApplyDamage(DamageInfo info) {
            vfx.Play();
            return new DamageResult {
                Hit = true
            };
        }
    }
}