using _03_Code.Interface;
using UnityEngine;

public class Damageable : MonoBehaviour, IDamageable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem vfx;

    public DamageResult ApplyDamage(DamageInfo info)
    {
        rb.AddForce(new Vector2(1f, 1f).normalized * info.KnockbackForce, ForceMode2D.Impulse);
        vfx.Play();
        return new DamageResult
        {
            Hit = true
        };
    }
}
