using _03_Code.Enemy.Interface;
using _03_Code.Player.Main;
using UnityEngine;

namespace _03_Code.Damageable {
    public class TestDamageable : MonoBehaviour, IDamageable {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private PlayerSystem pm;

        public DamageResult ApplyDamage(DamageInfo info) {
            rb.AddForce(new Vector2(6f * pm.MoveInput, 6f) * info.KnockbackForce, ForceMode2D.Impulse); 
            vfx.Play();
            return new DamageResult {
                Hit = true
            };
        }

        public void largePush() {
            rb.AddForce(
                new Vector2(Random.Range(4f, 10f) * (pm.MoveInput > 0f ? 1f : -1f), Random.Range(8f, 12f)) * 2.5f,
                ForceMode2D.Impulse);
        }
    }
}