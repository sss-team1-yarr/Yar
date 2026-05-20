using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class HandSpawner : MonoBehaviour {
        [SerializeField] private GameObject willUse;
        [SerializeField] private HandAttack handAttack;
        [SerializeField] private Transform[] spawnPoint;

        public bool canSpawn = true;
        
        private WaitForSeconds _willUseSkill = new WaitForSeconds(1f);
        private WaitForSeconds _skillMoveTime = new WaitForSeconds(2.5f);
        private int _spawnIndex;

        [ContextMenu("Spawn")]
        public void SpawnHand() {
            canSpawn = false;
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine() {
            GameObject hi = Instantiate(willUse);
            yield return _willUseSkill;
            Destroy(hi);
            
            GameObject hand = Instantiate(handAttack.gameObject, spawnPoint[_spawnIndex].position, spawnPoint[_spawnIndex].rotation);
            hand.GetComponent<Rigidbody2D>().linearVelocityX = _spawnIndex == 0 ? 100 : -100;
            yield return _skillMoveTime;
            _spawnIndex = _spawnIndex == 0 ? 1 : 0;
            canSpawn = true;
        }
    }
}