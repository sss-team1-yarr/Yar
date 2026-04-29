using System;
using _03_Code.Enemy.Common;
using _03_Code.Enemy.Interface;
using UnityEngine;

namespace _03_Code_Damageable {
    public class Damageable : MonoBehaviour, IDamageable {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private IsNearbyMe isNearbyMe;

        private void Reset() {
            rb = GetComponent<Rigidbody2D>();
            isNearbyMe = GetComponent<IsNearbyMe>();
        }

        public DamageResult ApplyDamage(DamageInfo info) {
            vfx.Play();
            StartCoroutine(isNearbyMe.Hit(info.KnockbackForce));
            return new DamageResult {
                Hit = true
            };
        }
    }
}