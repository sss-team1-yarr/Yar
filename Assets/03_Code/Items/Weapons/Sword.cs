using _03_Code.Enemy.Interface;
using _03_Code.Player.Main;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _03_Code.Items.Weapons {
    public class Sword : Weapon, IItem {
        [Header("Components")]
        [SerializeField] private Player.Main.Player owner;
        [SerializeField] private Attacks attack;
        [SerializeField] private Transform handTrm;
                        
        [Header("VFX")]
        [SerializeField] private ParticleSystem vfx;
        [SerializeField] private CinemachineImpulseSource impulseSource;
        
        [Header("Settings")]
        [SerializeField] private float cooldown = 0.2f;
        [SerializeField] private float damageRadius = 1.5f;
        [SerializeField] private float knockbackForce;
        [SerializeField] private ContactFilter2D targetFilter;
        
        private int _damageAmount = 5;
        
        private readonly Collider2D[] _hitBuffer = new Collider2D[10];
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;
        private float _ranKnockbackForce;

        private static readonly Vector3 AttackOffset = new(0.3f, 0.3f, 0f);

        public override bool CanUse => Time.time - _lastAttackTime >= cooldown;

        public override void Use(ItemUsingContext context) {
            base.Use(context);
            
            if (context.Input == 0) _isHoldingKey = context.Pressed;
        }

        private void Update() {
            Attack();
            DamageAmountChanged();
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + AttackOffset, damageRadius);
        }
        
        private void DamageAmountChanged() {
            if (!attack.IsFFF) {
                _damageAmount = GameManager.Instance.playerControl.Damage;
                return;
            }
            _damageAmount = GameManager.Instance.playerControl.UpperDamage;
        }
        
        private void Attack() {
            if (!_isHoldingKey || !CanUse) return;

            _lastAttackTime = Time.time;
            _isUpperAttack = !_isUpperAttack;
            handTrm.rotation = Quaternion.Euler(_isUpperAttack ? 180f : 0f, owner.transform.rotation.eulerAngles.y,
                owner.transform.rotation.eulerAngles.z);

            var center = transform.position + AttackOffset;

            targetFilter.useTriggers = true;

            var cnt = Physics2D.OverlapCircle(center, damageRadius, targetFilter, _hitBuffer);

            for (var i = 0; i < cnt; i++)
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable)) {
                    if (ReferenceEquals(damageable, owner)) continue;

                    _ranKnockbackForce = Random.Range(knockbackForce - 1.5f, knockbackForce + 1.5f);
                    var result = damageable.ApplyDamage(new DamageInfo {
                        DamageAmount = _damageAmount,
                        KnockbackForce = _ranKnockbackForce
                    });

                    if (result.Hit) impulseSource.GenerateImpulseWithForce(0.3f);
                }
        }
    }
}