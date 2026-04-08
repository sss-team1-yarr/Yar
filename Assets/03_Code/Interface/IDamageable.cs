using UnityEngine;

namespace _03_Code.Interface {
    public interface IDamageable{
        DamageResult ApplyDamage(DamageInfo info);
    }

    public struct DamageInfo {
        public float DamageAmount;
        public Vector3 AttackDirection;
        public float KnockbackForce;
    }

    public struct DamageResult {
        public bool Hit;
        public bool WasCrit;
        public float DamageAmount;
    }
}