using _03_Code.Interface;
using _3_Code.Player.Components;
using Unity.Cinemachine;
using UnityEngine;

namespace _03_Code.Items.Weapons {
    public class Sword : Weapon
    {
        [SerializeField] private InputReceiver input;
        [SerializeField] private float swingAngle = 120f;
        [SerializeField] private float cooldown = 0.2f;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private float damageRadius = 1.5f;
        [SerializeField] private Vector2 damageOffset;
        [SerializeField] private float damageAmount;
        [SerializeField] private float knockbackForce;
        [SerializeField] private ContactFilter2D targetFilter;
        [SerializeField] private CinemachineImpulseSource impulseSource;


        private readonly Collider2D[] _hitBuffer = new Collider2D[10];
        private Player.Player _owner;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;

        public override bool CanUse => Time.time - _lastAttackTime >= cooldown;


        public override void Use(ItemUsingContext context)
        {
            base.Use(context);
            if (context.Input == 0)
            {
                _isHoldingKey = context.Pressed; 
            }
        }

        public override void HoldItem(ItemUsingContext context)
        {
            base.HoldItem(context); 
            _owner = context.User;
        }

        private void Update()
        {
            CalcRotation();
            Attack();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (!_owner) return;
            var isRight = input.moveDir.x > 0;

            var attackPoint = _owner.transform.position; 
            attackPoint += (Vector3)input.moveDir.normalized * damageOffset.x;
            Vector3 rotatedDirection = new Vector2(-input.moveDir.y * (isRight ? -1 : 1),
                input.moveDir.x * (isRight ? 1 : -1)).normalized; 
            attackPoint += rotatedDirection * damageOffset.y;
            Gizmos.DrawWireSphere(attackPoint, damageRadius);
        }
        private void Attack()
        {
            if (!_isHoldingKey || !CanUse) return;
            _lastAttackTime = Time.time; 
            _isUpperAttack = !_isUpperAttack; 

            var direction = input.moveDir;
            var rotation = 45f - Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 

            vfx.Emit(new ParticleSystem.EmitParams
            {
                rotation = rotation
            }, 1);


            var isRight = direction.x > 0;

            var attackPoint = _owner.transform.position; 
            attackPoint += (Vector3)input.moveDir.normalized * damageOffset.x;
            Vector3 rotatedDirection = new Vector2(-input.moveDir.y * (isRight ? -1 : 1),
                input.moveDir.x * (isRight ? 1 : -1)).normalized; 
            attackPoint += rotatedDirection * damageOffset.y;


            var cnt = Physics2D.OverlapCircle(attackPoint, damageRadius, targetFilter, _hitBuffer); 
            for (var i = 0; i < cnt; i++) {
                if (!_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable)) continue;
                if (ReferenceEquals(damageable, _owner)) continue; 
                DamageResult result = damageable.ApplyDamage(new DamageInfo
                {
                    DamageAmount = damageAmount,
                    AttackDirection = input.moveDir,
                    KnockbackForce = knockbackForce,
                });
                if (result.Hit)
                {
                    impulseSource.GenerateImpulse(direction.normalized);
                }
            }
        }

        private void CalcRotation()
        {
            var direction = input.moveDir;
            var isRight = direction.x > 0;
            var dir = isRight ^ _isUpperAttack ? 1 : -1;

            transform.localScale = new Vector3(1f, dir, 1f);

            var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            var swing = swingAngle * (dir); 

            transform.rotation = Quaternion.Euler(0f, 0f, rotation + swing);
        }
    }
}
