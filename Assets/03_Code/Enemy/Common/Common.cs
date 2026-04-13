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
        [SerializeField] private int damage;
        [SerializeField] private HpManager hp;
        [SerializeField] private PlayerRenderer playerRenderer;
        [SerializeField] private IsNearbyMe rc;
        
        private void Update() {
            transform.rotation = Quaternion.Euler(0f, rc.MoveDir.x>0f?0f:180f, 0f);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            hp.UpdateHp(damage);
        }
    }
}