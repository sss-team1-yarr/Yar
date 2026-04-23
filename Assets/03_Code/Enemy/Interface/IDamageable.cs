namespace _03_Code.Enemy.Interface {
    public interface IDamageable {
        DamageResult ApplyDamage(DamageInfo info);
    }

    public struct DamageInfo {
        public float DamageAmount;
        public float KnockbackForce;
    }

    public struct DamageResult {
        public bool Hit;
        public bool WasCrit;
        public float DamageAmount;
    }
}