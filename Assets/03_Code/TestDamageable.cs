using _03_Code.Interface;
using UnityEngine;

namespace _03_Code {
    public class TestDamageable : MonoBehaviour, IDamageable {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem vfx;

        public DamageResult ApplyDamage(DamageInfo info) {
            rb.AddForce(new Vector2(Random.Range(5f, 7f), Random.Range(3f, 5f)) * info.KnockbackForce,
                ForceMode2D.Impulse);
            vfx.Play();
            return new DamageResult {
                Hit = true
            };
        }

        public void largePush()
        {
            rb.AddForce(new Vector2(Random.Range(4f, 10f), Random.Range(8f, 12f)) * 2.5f, ForceMode2D.Impulse);
        }
    }
}