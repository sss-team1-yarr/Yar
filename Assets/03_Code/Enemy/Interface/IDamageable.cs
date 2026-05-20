namespace _03_Code.Enemy.Interface {
    public interface IDamageable {
        DamageResult ApplyDamage(int damageAmount, bool isCharge);
    }

    public struct DamageResult {
        public bool Hit;
    }
}