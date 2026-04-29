using System;
using _03_Code.Player.Components;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private PlayerRenderer _pr;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed = 5f;
    
    private Vector2 _moveDir;

    private void Reset() {
        _rb = GetComponent<Rigidbody2D>();
        _pr = GetComponent<PlayerRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate() {
        _rb.linearVelocity = _moveDir.normalized * _speed;
    }

    private void LateUpdate() {
        _moveDir = _player.position - transform.position;
    }
}
