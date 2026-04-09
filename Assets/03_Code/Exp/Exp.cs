using System;
using UnityEngine;

namespace _03_Code.Exp {
    public class Exp : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed;
        private GameObject _playerTrm;

        private Vector2 _moveDir;

        private void Start() {
            _playerTrm = GameObject.FindWithTag("Player");
        }

        private void FixedUpdate() {
            _moveDir = _playerTrm.transform.position - transform.position;
            _moveDir.Normalize();
            rb.linearVelocity = _moveDir * moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Destroy(gameObject);
        }
    }
}