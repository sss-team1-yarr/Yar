using System;
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

        private Vector2 _moveDir;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if(playerTrm == null) return;
            _moveDir = playerTrm.position - transform.position;
            _moveDir.Normalize(); 
        }

        private void FixedUpdate()
        {
            rb.linearVelocityX = _moveDir.x * moveSpeed; 
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Destroy(collision.gameObject);
        }
    }
}