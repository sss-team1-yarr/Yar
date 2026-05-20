using System.Collections;
using _03_Code.Player.Components;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class Meteor : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private ParticleSystem tail;
        [SerializeField] private Vector2 velocityValue = new(-9.8f, -19.6f);

        private bool _isNotGround = true;

        private void OnEnable() {
            rb.linearVelocity = velocityValue;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Ground"))
                StartCoroutine(Crash());
            else if (other.CompareTag("Player") && _isNotGround) HpManager.Instance.Damage(10);
        }

        private IEnumerator Crash() {
            _isNotGround = false;
            yield return new WaitForSeconds(0.04f);
            rb.linearVelocity = Vector2.zero;
            tail.Stop(true);
            particle.Play();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}