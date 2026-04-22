using _03_Code.Interface;
using _03_Code.Player.Components;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _03_Code.Items.Weapons {
    public class Sword : Weapon
    {
        [SerializeField] private float cooldown = 0.2f;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private float damageRadius = 1.5f;
        [SerializeField] private float damageAmount;
        [SerializeField] private float knockbackForce;
        [SerializeField] private ContactFilter2D targetFilter;
        [SerializeField] private Transform handTrm;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        private readonly Collider2D[] _hitBuffer = new Collider2D[10];
        [SerializeField] Player.Player _owner;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;
        private float ranKnockbackForce;

        private static readonly Vector3 AttackOffset = new Vector3(0.3f, 0.3f, 0f);

        public override bool CanUse => Time.time - _lastAttackTime >= cooldown;

        public override void Use(ItemUsingContext context)
        {
            base.Use(context);

            if (context.Input == 0)
            {
                _isHoldingKey = context.Pressed;
            }
        }

        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            if (!_isHoldingKey || !CanUse) return;

            _lastAttackTime = Time.time;
            _isUpperAttack = !_isUpperAttack;
            handTrm.rotation = Quaternion.Euler(_isUpperAttack ? 180f : 0f, _owner.transform.rotation.eulerAngles.y, _owner.transform.rotation.eulerAngles.z);

            Vector3 center = transform.position + AttackOffset;

            targetFilter.useTriggers = true; 
            
            int cnt = Physics2D.OverlapCircle(center, damageRadius, targetFilter, _hitBuffer);

            for (int i = 0; i < cnt; i++)
            {
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable))
                {
                    if (ReferenceEquals(damageable, _owner)) continue;
                                        
                    ranKnockbackForce = Random.Range(knockbackForce - 1.5f, knockbackForce + 1.5f);
                    DamageResult result = damageable.ApplyDamage(new DamageInfo
                    {
                        DamageAmount = damageAmount,
                        KnockbackForce = ranKnockbackForce,
                    });
                    
                    if (result.Hit)
                    {
                        impulseSource.GenerateImpulseWithForce(1f);
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + AttackOffset, damageRadius);
        }
    }
}