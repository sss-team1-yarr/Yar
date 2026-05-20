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

        [Header("Components")] 
        [SerializeField] private Player.Main.Player owner;
        [SerializeField] private Attacks attack;
        [SerializeField] private Transform handTrm;
        [SerializeField] private SlashSpawner slashSpawner;
        [SerializeField] private GameObject sword;

        [Header("VFX")] 
        [SerializeField] private ParticleSystem attackVfx;
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
        private float _cooltime;
        private float _chargingCooltime;

        private int _damageAmount;
        private int _chargeDamageAmount;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;
        private float _radius;
        private float _chargingRadius;

        private bool _isCharge = false;
        private bool _isFullCharge = false;
        private float _chargingTime = 0;
        private bool _isAttacking = false;

        private ParticleSystem.ShapeModule _chargeCircleShape;
        private bool _wasHoldingKey;

        public bool CanUse => Time.time - _lastAttackTime >= _cooltime;

        private void Start()
        {
            Init();
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

        private void Init()
        {
            _chargeCircleShape = chargingCircleVfx.shape;
            _cooltime = GameManager.Instance.playerControl.AttackCoolTime;
            _radius = GameManager.Instance.playerControl.AttackRadius;
            _chargingCooltime = GameManager.Instance.playerControl.ChargingAttackCoolTime;
            _chargingRadius = GameManager.Instance.playerControl.ChargingAttackRadius;
        }
        
        private void DamageAmountChanged() {
            if (!attack.IsFFF) {
                _damageAmount = GameManager.Instance.playerControl.Damage;
                _chargeDamageAmount = GameManager.Instance.playerControl.UpperDamage;
                return;
            }

            _damageAmount = Mathf.FloorToInt(GameManager.Instance.playerControl.UpperDamage * 1.5f);
            _chargeDamageAmount = Mathf.FloorToInt(GameManager.Instance.playerControl.UpperDamage * 1.5f);
        }
        
        private void Charging()
        {
            if (!CanUse) return; 
            
            // 누르기 시작한 순간
            if (_isHoldingKey && !_wasHoldingKey)
            {
                _isCharge = true;
                _isFullCharge = false;
                _chargingTime = 0f;

                _chargeCircleShape.radius = startCircleRadius;
                chargingCircleVfx.Play();
            }

            // 누르고 있는 동안
            if (_isHoldingKey && _isCharge && !_isFullCharge)
            {
                _chargingTime += Time.deltaTime;

                float charge01 = Mathf.Clamp01(_chargingTime / needChargeTime);

                float radius = Mathf.Lerp(startCircleRadius, endCircleRadius, charge01);
                _chargeCircleShape.radius = radius;

                if (_chargingTime >= needChargeTime)
                {
                    _isFullCharge = true;

                    chargingCircleVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    chargingCompleteVfx.Play();

                    sword.transform.localScale = new Vector3(0.2f, 0.14f, 0.14f);
                }
            }

            // 키를 뗀 순간
            if (!_isHoldingKey && _wasHoldingKey)
            {
                _isCharge = false;

                chargingCircleVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                
                StartCoroutine(Attack(_isFullCharge));
                
                _chargingTime = 0f;
                _isFullCharge = false;
            }

            _wasHoldingKey = _isHoldingKey;
        }

        private IEnumerator Attack(bool fullCharge)
        {
            if (_isAttacking) yield break; 
            
            _isAttacking = true;
            _lastAttackTime = Time.time;

            transform.DOBlendableLocalRotateBy(new Vector3(0,0,-70),fullCharge? .2f : .1f).SetEase(Ease.OutExpo).OnComplete(() => 
                transform.DOBlendableLocalRotateBy(new Vector3(0,0,70),fullCharge? .6f : .4f).SetEase(Ease.InQuad)
            );

            float dir = GameManager.Instance.playerControl.RotationRight ? 1f : -1f;
            var center = transform.position + AttackOffset * dir;
            targetFilter.useTriggers = true;
            var cnt = Physics2D.OverlapCircle(center, fullCharge? _chargingRadius : _radius, targetFilter, _hitBuffer);

            slashSpawner.BaseScale = fullCharge? _chargingRadius : _radius;
            
            if (cnt == 0)
                SlashSpawner.Instance.Attack(SlashSpawner.SlashStyle.Single);

            for (var i = 0; i < cnt; i++)
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable)) {
                    var result = damageable.ApplyDamage(_chargeDamageAmount);

                    if (result.Hit)
                    {
                        impulseSource.GenerateImpulseWithForce(0.02f);
                        SlashSpawner.Instance.Attack(SlashSpawner.SlashStyle.Combo);
                    }
                }
            
            yield return new WaitForSeconds(fullCharge? _chargingCooltime : _cooltime);
            sword.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);  
            _isAttacking = false;
        }
    }
}