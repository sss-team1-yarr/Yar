using System.Collections;
using _03_Code.Player.Components;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class HandAttack : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        

        private readonly WaitForSeconds _skillSeconds = new WaitForSeconds(2f);

        private void OnEnable() {
            StartCoroutine(HandLifeCoroutine());
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) HpManager.Instance.Damage(10);
        }

        private IEnumerator HandLifeCoroutine() {
            yield return _skillSeconds;
            Destroy(gameObject);
        }
    }
}