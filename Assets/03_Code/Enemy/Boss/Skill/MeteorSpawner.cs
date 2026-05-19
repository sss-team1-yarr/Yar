using System.Collections;
using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class MeteorSpawner : MonoBehaviour {
        [SerializeField] private GameObject meteorPrefab;
        [SerializeField] private GameObject[] spawnPoints;
        [SerializeField] private float spawnDuration;

        private float _durationManager;
        private bool _isSpawning;
        private float _time;

        private void Update() {
            _time += Time.deltaTime;
            if (_time > _durationManager && _isSpawning) {
                _durationManager += spawnDuration;
                _time = 0f;
                Instantiate(meteorPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform);
            }
        }

        public void SpawnMeteor() {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn() {
            _isSpawning = true;
            yield return new WaitForSeconds(3f);
            _isSpawning = false;
        }
    }
}