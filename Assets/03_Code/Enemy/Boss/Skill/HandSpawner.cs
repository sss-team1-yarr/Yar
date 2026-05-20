using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class HandSpawner : MonoBehaviour {
        [SerializeField] private GameObject willUse;
        [SerializeField] private HandAttack handAttack;
        [SerializeField] private Transform[] spawnPoint;
        
        private WaitForSeconds _skillSeconds = new WaitForSeconds(1f);
        private int _spawnIndex;

        [ContextMenu("Spawn")]
        public void SpawnHand() {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine() {
            GameObject hi = Instantiate(willUse);
            yield return _skillSeconds;
            Destroy(hi);
            
            GameObject hand = Instantiate(handAttack.gameObject, spawnPoint[_spawnIndex].position, spawnPoint[_spawnIndex].rotation);
            hand.GetComponent<Rigidbody2D>().linearVelocityX = _spawnIndex == 0 ? 100 : -100;
            _spawnIndex = _spawnIndex == 0 ? 1 : 0;
        }
    }
}