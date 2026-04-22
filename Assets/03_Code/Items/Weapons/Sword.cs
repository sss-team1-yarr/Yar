using _03_Code.Interface;
using _03_Code.Player.Components;
using Unity.Cinemachine;
using UnityEngine;

namespace _03_Code.Items.Weapons {
    public class Sword : Weapon {
        [SerializeField] private float cooldown = 0.2f;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private float damageRadius = 1.5f;
        [SerializeField] private float damageAmount;
        [SerializeField] private float knockbackForce;
        [SerializeField] private ContactFilter2D targetFilter;
        [SerializeField] private Transform handTrm;
        
        private readonly Collider2D[] _hitBuffer = new Collider2D[10];
        private Player.Player _owner;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;


        public override bool CanUse => Time.time - _lastAttackTime >= cooldown;


        public override void Use(ItemUsingContext context) {
            base.Use(context);
            if (context.Pressed) Debug.Log("Attack!");
            if (context.Input == 0) _isHoldingKey = context.Pressed;
        }

        public override void HoldItem(ItemUsingContext context) {
            base.HoldItem(context);
            _owner = context.User;
        }

        private void Update() {
            Attack();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0.3f, 0.3f, 0), damageRadius);
        }
        
        private void Attack()
        {
            if (!_isHoldingKey || !CanUse) return;
            _lastAttackTime = Time.time; 
            _isUpperAttack = !_isUpperAttack;
            handTrm.rotation = Quaternion.Euler(_isUpperAttack ? 180f : 0f, 0f, 0f);

            vfx.Emit(new ParticleSystem.EmitParams(), 5);

            int cnt = Physics2D.OverlapCircle(transform.position + new Vector3(0.3f, 0.3f, 0).normalized, damageRadius, targetFilter, _hitBuffer);
            for (int i = 0; i < cnt; i++)
            {
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable))
                {
                    DamageResult result = damageable.ApplyDamage(new DamageInfo
                    {
                        DamageAmount = damageAmount,
                        KnockbackForce = knockbackForce,
                    });
                }
            }
        }
    }
}