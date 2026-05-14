using System;
using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class Meteor : MonoBehaviour {
        [SerializeField] private Rigidbody2D boomTrigger;
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private ParticleSystem tail;
        
        private void OnEnable() {
            boomTrigger.linearVelocity = new Vector2(-9.8f,-19.6f);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Ground")) {
                StartCoroutine(Boom());
            }else if (other.CompareTag("Player")) {
                
            }
        }

        private IEnumerator Boom() {
            boomTrigger.linearVelocity = Vector2.zero;
            tail.Stop(true);
            particle.Play();
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        }
    }
}