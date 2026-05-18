using System.Collections;
using _03_Code.Enemy.Interface;
using _03_Code.Player.Main;
using Unity.Cinemachine;
using UnityEngine;

namespace _03_Code.Items.Weapons {
    public class Sword : Weapon {
        [SerializeField] private Vector3 AttackOffset = new(0.3f, 0.3f, 0f);

        [Header("Components")] [SerializeField]
        private Player.Main.Player owner;

        [SerializeField] private Attacks attack;
        [SerializeField] private Transform handTrm;

        [Header("VFX")] [SerializeField] private ParticleSystem vfx;

        [SerializeField] private CinemachineImpulseSource impulseSource;

        [Header("Settings")] [SerializeField] private float knockbackForce;

        [SerializeField] private ContactFilter2D targetFilter;

        private readonly Collider2D[] _hitBuffer = new Collider2D[10];
        private float _cooltime;

        private int _damageAmount;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;
        private float _radius;

        private bool CanUse => Time.time - _lastAttackTime >= _cooltime;

        private void Start() {
            _cooltime = GameManager.Instance.playerControl.AttackCoolTime;
            _radius = GameManager.Instance.playerControl.AttackRadius;
        }

        private void Update() {
            StartCoroutine(Attack());
            DamageAmountChanged();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + AttackOffset, _radius);
        }

        public override void Use(ItemUsingContext context) {
            base.Use(context);

            if (context.Input == 0) _isHoldingKey = context.Pressed;
        }

        private void DamageAmountChanged() {
            if (!attack.IsFFF) {
                _damageAmount = GameManager.Instance.playerControl.Damage;
                return;
            }

            _damageAmount = GameManager.Instance.playerControl.UpperDamage;
        }

        private IEnumerator Attack() {
            if (!_isHoldingKey || !CanUse) yield break;

            _lastAttackTime = Time.time;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                -35f);

            var center = transform.position + AttackOffset;

            targetFilter.useTriggers = true;

            var cnt = Physics2D.OverlapCircle(center, _radius, targetFilter, _hitBuffer);

            for (var i = 0; i < cnt; i++)
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable)) {
                    var result = damageable.ApplyDamage(_damageAmount);

                    if (result.Hit) impulseSource.GenerateImpulseWithForce(0.1f);
                }

            yield return new WaitForSeconds(_cooltime);

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                35f);
        }
    }
}