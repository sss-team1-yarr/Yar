using System.Collections;
using _03_Code.Enemy.Interface;
using _03_Code.Manager;
using _03_Code.Player.Main;
using _03_Code.VFX;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace _03_Code.Items.Weapons {
    public class Sword : Weapon {
        [SerializeField] private Vector3 AttackOffset = new(0.3f, 0.3f, 0f);

        [Header("Components")] [SerializeField]
        private Player.Main.Player owner;

        [SerializeField] private Attacks attack;
        [SerializeField] private Transform handTrm;
        [SerializeField] private SlashSpawner slashSpawner;
        [SerializeField] private GameObject sword;

        [Header("VFX")] [SerializeField] private ParticleSystem attackVfx;

        [SerializeField] private ParticleSystem chargingVfx;
        [SerializeField] private ParticleSystem chargingCircleVfx;
        [SerializeField] private ParticleSystem chargingCompleteVfx;
        [SerializeField] private CinemachineImpulseSource impulseSource;

        [Header("Settings")] [SerializeField] private float knockbackForce;
        [SerializeField] private float needChargeTime = 1f;
        [SerializeField] private float startCircleRadius = 2.5f;
        [SerializeField] private float endCircleRadius = 0.2f;

        [SerializeField] private ContactFilter2D targetFilter;

        private readonly Collider2D[] _hitBuffer = new Collider2D[10];

        private ParticleSystem.ShapeModule _chargeCircleShape;
        private float _chargingRadius;
        private float _chargingTime;
        private float _cooltime;

        private int _damageAmount;
        private bool _isAttacking;

        private bool _isCharge;
        private bool _isFullCharge;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;
        private float _radius;
        private bool _wasHoldingKey;

        public bool CanUse => Time.time - _lastAttackTime >= _cooltime;

        private void Awake() {
            _chargeCircleShape = chargingCircleVfx.shape;
        }

        private void Start() {
            _cooltime = GameManager.Instance.playerControl.AttackCoolTime;
            _radius = GameManager.Instance.playerControl.AttackRadius;
            _chargingRadius = GameManager.Instance.playerControl.ChargingAttackRadius;
        }

        private void Update() {
            Charging();
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

        private void Charging() {
            if (!CanUse) return;

            // 누르기 시작한 순간
            if (_isHoldingKey && !_wasHoldingKey) {
                _isCharge = true;
                _isFullCharge = false;
                _chargingTime = 0f;

                _chargeCircleShape.radius = startCircleRadius;
                chargingCircleVfx.Play();
            }

            // 누르고 있는 동안
            if (_isHoldingKey && _isCharge && !_isFullCharge) {
                _chargingTime += Time.deltaTime;

                var charge01 = Mathf.Clamp01(_chargingTime / needChargeTime);

                var radius = Mathf.Lerp(startCircleRadius, endCircleRadius, charge01);
                _chargeCircleShape.radius = radius;

                if (_chargingTime >= needChargeTime) {
                    _isFullCharge = true;

                    chargingCircleVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    chargingCompleteVfx.Play();

                    sword.transform.localScale = new Vector3(0.2f, 0.14f, 0.14f);
                }
            }

            // 키를 뗀 순간
            if (!_isHoldingKey && _wasHoldingKey) {
                _isCharge = false;

                chargingCircleVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);


                if (_isFullCharge)
                    StartCoroutine(ChargingAttack());
                else
                    StartCoroutine(NormalAttack());

                _chargingTime = 0f;
                _isFullCharge = false;
            }

            _wasHoldingKey = _isHoldingKey;
        }

        private IEnumerator NormalAttack() {
            if (_isAttacking) yield break;

            _lastAttackTime = Time.time;

            transform.DOBlendableLocalRotateBy(new Vector3(0, 0, -70), .1f).SetEase(Ease.OutExpo).OnComplete(() =>
                transform.DOBlendableLocalRotateBy(new Vector3(0, 0, 70), .4f).SetEase(Ease.InQuad)
            );

            var dir = GameManager.Instance.playerControl.RotationRight ? 1f : -1f;
            var center = transform.position + AttackOffset * dir;
            targetFilter.useTriggers = true;
            var cnt = Physics2D.OverlapCircle(center, _radius, targetFilter, _hitBuffer);

            slashSpawner.BaseScale = _radius;

            if (cnt == 0)
                SlashSpawner.Instance.Attack(SlashSpawner.SlashStyle.Single);

            for (var i = 0; i < cnt; i++)
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable)) {
                    var result = damageable.ApplyDamage(_damageAmount);

                    if (result.Hit) impulseSource.GenerateImpulseWithForce(0.02f);
                }

            yield return new WaitForSeconds(_cooltime + 0.1f);

            _isAttacking = false;
        }

        private IEnumerator ChargingAttack() {
            if (_isAttacking) yield break;

            _isAttacking = true;
            _lastAttackTime = Time.time;

            transform.DOBlendableLocalRotateBy(new Vector3(0, 0, -70), .2f).SetEase(Ease.OutExpo).OnComplete(() =>
                transform.DOBlendableLocalRotateBy(new Vector3(0, 0, 70), .6f).SetEase(Ease.InQuad)
            );

            var dir = GameManager.Instance.playerControl.RotationRight ? 1f : -1f;
            var center = transform.position + AttackOffset * dir;
            targetFilter.useTriggers = true;
            var cnt = Physics2D.OverlapCircle(center, _chargingRadius, targetFilter, _hitBuffer);

            slashSpawner.BaseScale = _chargingRadius;

            if (cnt == 0)
                SlashSpawner.Instance.Attack(SlashSpawner.SlashStyle.Single);

            for (var i = 0; i < cnt; i++)
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable)) {
                    var result = damageable.ApplyDamage(_damageAmount);

                    if (result.Hit) impulseSource.GenerateImpulseWithForce(0.1f);
                }

            yield return new WaitForSeconds(0.9f);

            sword.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
            _isAttacking = false;
            //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,35f);
        }

        // private IEnumerator DownSword()
        // {
        //     for (float i = 0; i < 1f; i += Time.time)
        //     {
        //         float radius = Mathf.Lerp(35, -35, i);
        //         transform.rotation = Quaternion.Euler(
        //             transform.rotation.eulerAngles.x,
        //             transform.rotation.eulerAngles.y,
        //             radius);
        //         
        //         yield return new WaitForSeconds(0.01f);
        //     }
        // }
    }
}