namespace _03_Code.Enemy.Interface {
    public interface IDamageable {
        DamageResult ApplyDamage(int damageAmount);
    }

    public struct DamageResult {
        public bool Hit;
    }
}