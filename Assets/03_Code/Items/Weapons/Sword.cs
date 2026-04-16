using _03_Code.Interface;
using _03_Code.Player.Components;
using Unity.Cinemachine;
using UnityEngine;

namespace _03_Code.Items.Weapons {
    public class Sword : Weapon {
        [SerializeField] private float swingAngle = 120f;
        [SerializeField] private float cooldown = 0.2f;
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private float damageRadius = 1.5f;
        [SerializeField] private Vector2 damageOffset;
        [SerializeField] private float damageAmount;
        [SerializeField] private float knockbackForce;
        [SerializeField] private ContactFilter2D targetFilter;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [SerializeField] private Transform handTrm;

        [SerializeField] private InputReceiver input;

        private readonly Collider2D[] _hitBuffer = new Collider2D[10];
        private Player.Player _owner;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;
        private bool isRight;

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
            CalcRotation();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (_owner)
            {
                bool isRight = input.onMoveInputVec2.x > 0;

                Vector3 attackPoint = _owner.transform.position; 
                attackPoint += (Vector3)input.onMoveInputVec2.normalized * damageOffset.x;
                Vector3 rotatedDirection = new Vector2(-input.onMoveInputVec2.y * (isRight ? -1 : 1),
                    input.onMoveInputVec2.x * (isRight ? 1 : -1)).normalized; 
                attackPoint += rotatedDirection * damageOffset.y;
                Gizmos.DrawWireSphere(attackPoint, damageRadius);

            }
        }
        private void Attack()
        {
            if (!_isHoldingKey || !CanUse) return;
            _lastAttackTime = Time.time; 
            _isUpperAttack = !_isUpperAttack; 

            Vector2 direction = input.onMoveInputVec2;
            float rotation = 45f - Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            vfx.Emit(new ParticleSystem.EmitParams
            {
                rotation = rotation
            }, 1);


            bool isRight = direction.x > 0;

            Vector3 attackPoint = _owner.transform.position; 
            attackPoint += (Vector3)input.onMoveInputVec2.normalized * damageOffset.x;
            Vector3 rotatedDirection = new Vector2(-input.onMoveInputVec2.y * (isRight ? -1 : 1),
                input.onMoveInputVec2.x * (isRight ? 1 : -1)).normalized;
            attackPoint += rotatedDirection * damageOffset.y;

            int cnt = Physics2D.OverlapCircle(attackPoint, damageRadius, targetFilter, _hitBuffer);
            for (int i = 0; i < cnt; i++)
            {
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable))
                {
                    if (ReferenceEquals(damageable, _owner)) continue;
                    DamageResult result = damageable.ApplyDamage(new DamageInfo
                    {
                        DamageAmount = damageAmount,
                        KnockbackForce = knockbackForce,
                    });
                    if (result.Hit)
                    {
                        impulseSource.GenerateImpulse(direction.normalized);
                    }
                }
            }
        }

        private void CalcRotation()
        {
            if (handTrm == null) return;

            Vector2 direction = input.onMoveInputVec2;
            bool isRight = direction.x > 0;
            int dir = isRight ^ _isUpperAttack ? 1 : -1;

            handTrm.localScale = new Vector3(1f, dir, 1f);

            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
            float swing = swingAngle * (dir);

            handTrm.rotation = Quaternion.Euler(0f, 0f, rotation + swing);
        }
    }
}