using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class HandSpawner : MonoBehaviour {
        [SerializeField] private GameObject willUse;
        [SerializeField] private HandAttack handAttack;
        [SerializeField] private Transform[] spawnPoint;

        private readonly WaitForSeconds _skillSeconds = new(1f);
        private int _spawnIndex;

        [ContextMenu("Spawn")]
        public void SpawnHand() {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine() {
            var hi = Instantiate(willUse);
            yield return _skillSeconds;
            Destroy(hi);

            var hand = Instantiate(handAttack.gameObject, spawnPoint[_spawnIndex].position,
                spawnPoint[_spawnIndex].rotation);
            hand.GetComponent<Rigidbody2D>().linearVelocityX = _spawnIndex == 0 ? 100 : -100;
            _spawnIndex = _spawnIndex == 0 ? 1 : 0;
        }
    }
}