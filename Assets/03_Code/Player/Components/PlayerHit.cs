using System.Collections;
using _03_Code.Enemy.Common;
using _03_Code.Player.VFX;
using UnityEngine;

namespace _03_Code.Player.Components {
    public class PlayerHit : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private HitParticle hitParticle;
        [SerializeField] private ParticleSystem hitVFX;

        public bool IsApproach { get; private set; }

        private void Reset() {
            rb = GetComponent<Rigidbody2D>(); //?
        }

        public IEnumerator Approach(Vector2 direction, Monster em) {
            if (IsApproach) yield break;

            IsApproach = true;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * em.ApproachForce, ForceMode2D.Impulse);
            HpManager.Instance.Damage(em.ApproachDamage);

            hitParticle.PlayHitEffect();
            hitVFX.Play();

            yield return new WaitForSeconds(em.ApproachTime);
            IsApproach = false;
        }

        public IEnumerator Approach() {
            if (IsApproach) yield break;

            rb.linearVelocity = Vector2.zero;

            HpManager.Instance.Damage(5);

            hitParticle.PlayHitEffect();
            hitVFX.Play();
        }
    }
}