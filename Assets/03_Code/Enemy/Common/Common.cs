using System;
using System.Collections;
using _03_Code.Player.Components;
using _03_Code.Player.Main;
using _03_Code.SO;
using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class Common : MonoBehaviour {
        [SerializeField] private EnemySO enemyData;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private int damage;
        [SerializeField] private HpManager hp;
        [SerializeField] private PlayerRenderer playerRenderer;
        [SerializeField] private float speed = 2f;
        [SerializeField] private float enemyKnockBackForce = 5f;
        [SerializeField] private float hitDuration = 0.1f;

        private IsNearbyMe _scan;
        private bool _isHit;

        private void Awake() {
            _scan = GetComponent<IsNearbyMe>();
        }

        private void Update() {
            transform.rotation = Quaternion.Euler(0f, _scan.MoveDir.x > 0f ? 0f : 180f, 0f);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            hp.UpdateHp(damage);
        }

        private void FixedUpdate() {
            if(!_isHit)
                rb.linearVelocity = _scan.MoveDir.normalized * speed;
        }

        public IEnumerator Hit(float knockbackForce) {
            if (_isHit) yield break; 
            _isHit = true;
            float dir = transform.position.x > GameManager.Instance.playerControl.transform.position.x ? 1f : -1f;
            
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(knockbackForce * dir * enemyKnockBackForce, knockbackForce * enemyKnockBackForce), 
                ForceMode2D.Impulse);
            
            yield return new WaitForSeconds(hitDuration);
            _isHit = false; 
        }
    }
}