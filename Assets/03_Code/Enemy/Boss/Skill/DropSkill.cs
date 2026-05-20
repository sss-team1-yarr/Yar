using System;
using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class DropSkill : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform playerTrm;
        
        private WaitForSeconds _waitForAttack =  new WaitForSeconds(3f);
        
        private Vector2 _moveDir;
        private bool _canTargeting = false;

        private void Start() {
            gameObject.SetActive(false);
        }

        public void StartAttack() {
            transform.position = new Vector2(-30f, -18f);
            gameObject.SetActive(true);
            StartCoroutine(StartDropSkill());
        }

        private void FixedUpdate() {
            if (!_canTargeting) return;
            
            _moveDir = playerTrm.position - transform.position;
            rb.linearVelocityX = moveSpeed * _moveDir.x;
        }

        private IEnumerator StartDropSkill() {
            for (int i = 0; i < 4; i++) {
                rb.linearVelocityY = 200;
                yield return new WaitForSeconds(0.25f);
                rb.linearVelocityY = 0f;
                _canTargeting = true;
                yield return _waitForAttack;
                _canTargeting = false;
                rb.linearVelocityX = 0;
                yield return new WaitForSeconds(0.5f);
                rb.linearVelocityY = -200f;
                yield return new WaitForSeconds(0.25f);
                rb.linearVelocityY = 0f;
            }
            
            Destroy(gameObject);
        }
    }
}