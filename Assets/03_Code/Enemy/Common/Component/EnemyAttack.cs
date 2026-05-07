using System;
using System.Collections;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

namespace _03_Code.Enemy.Common.Component
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private EnemyMove em; 
        
        private void Reset()
        {
            rb = GetComponent<Rigidbody2D>();
            em = GetComponent<EnemyMove>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(GameManager.Instance.playerHit.Approach(em.Direction));
            }
        }
    }
}
