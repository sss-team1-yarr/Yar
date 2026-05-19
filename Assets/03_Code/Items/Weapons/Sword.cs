using System.Collections;
using _03_Code.Enemy.Interface;
using _03_Code.Player.Main;
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

        private int _damageAmount;
        private bool _isHoldingKey;
        private bool _isUpperAttack;
        private float _lastAttackTime;
        private float _radius;

        private bool _isCharge = false;
        private bool _isFullCharge = false;
        private float _chargingTime = 0;

        private bool _isDownSword = false;
        




        private ParticleSystem.ShapeModule _chargeCircleShape;
        private bool _wasHoldingKey;

        private void Awake()
        {
            _chargeCircleShape = chargingCircleVfx.shape;
        }
        
        public bool CanUse => Time.time - _lastAttackTime >= _cooltime;

        private void Start() {
            _cooltime = GameManager.Instance.playerControl.AttackCoolTime;
            _radius = GameManager.Instance.playerControl.AttackRadius;
        }

        private void Update() {
            _Charging();
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
        
        private void _Charging()
        {
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
                }
            }

            // 키를 뗀 순간
            if (!_isHoldingKey && _wasHoldingKey)
            {
                _isCharge = false;

                chargingCircleVfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

                StartCoroutine(ChargingAttack(_chargingTime));

                _chargingTime = 0f;
                _isFullCharge = false;
            }

            _wasHoldingKey = _isHoldingKey;
        }

        private IEnumerator ChargingAttack(float charge)
        {
            if (charge < needChargeTime) yield break;
            
            _lastAttackTime = Time.time;
            _isDownSword = true;

            transform.DORotate(new Vector3(0,0,-35),.2f);

            var center = transform.position + AttackOffset  /*GameManager.Instance.*/;
            targetFilter.useTriggers = true;
            var cnt = Physics2D.OverlapCircle(center, _radius, targetFilter, _hitBuffer);
            
            if (cnt == 0)
                SlashSpawner.Instance.Attack(SlashSpawner.SlashStyle.Single);

            for (var i = 0; i < cnt; i++)
                if (_hitBuffer[i].TryGetComponent<IDamageable>(out var damageable)) {
                    var result = damageable.ApplyDamage(_damageAmount);

                    if (result.Hit) impulseSource.GenerateImpulseWithForce(0.1f);
                }
            
            yield return new WaitForSeconds(_cooltime);
            
            transform.DORotate(new Vector3(0,0,35),0.8f);
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