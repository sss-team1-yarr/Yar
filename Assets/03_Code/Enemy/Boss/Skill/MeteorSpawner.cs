using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class MeteorSpawner : MonoBehaviour {
        [SerializeField] private GameObject meteorPrefab;
        [SerializeField] private GameObject[] spawnPoints;
        [SerializeField] private float spawnDuration;

        private float _durationManager;
        private float _time = 0f;
        private bool _isSpawning = false;

        private void Update() {
            _time += Time.deltaTime;
            if (_time > _durationManager&&_isSpawning) {
                _durationManager += spawnDuration;
                _time = 0f;
                Instantiate(meteorPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform);
            }
        }

        public void SpawnMeteor() {
            StartCoroutine(Coroutine());
        }

        private IEnumerator Coroutine() {
            _isSpawning = true;
            yield return new WaitForSeconds(3f);
            _isSpawning = false;
        }
    }
}