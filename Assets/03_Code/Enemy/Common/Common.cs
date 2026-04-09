using System;
using _03_Code.Player.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _03_Code.Enemy.Common
{
    public class Common : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private Transform playerTrm;
        [SerializeField] private int damage;
        [SerializeField] private HpManager hp;

        private Vector2 _moveDir;

        private void Update()
        {
            if(playerTrm == null) return;
            _moveDir = playerTrm.position - transform.position;
            _moveDir.Normalize(); 
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = _moveDir * moveSpeed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            hp.UpdateHp(damage);
        }
    }
}