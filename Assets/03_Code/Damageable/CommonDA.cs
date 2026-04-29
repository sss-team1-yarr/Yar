using _03_Code.Enemy.Interface;
using _03_Code.Player.Main;
using UnityEngine;

namespace _03_Code.Damageable {
    public class CommonDA : MonoBehaviour, IDamageable
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private PlayerMove pm;

        private void Reset() {
            rb = GetComponent<Rigidbody2D>();
            pm = GetComponent<PlayerMove>();
        }

        public DamageResult ApplyDamage(DamageInfo info) {
            vfx.Play();
            return new DamageResult {
                Hit = true
            };
        }
    }
}
