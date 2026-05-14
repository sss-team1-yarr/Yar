using UnityEngine;

namespace _03_Code.Enemy.Boss.Skill {
    public class MeteorSpawner : MonoBehaviour {
        [SerializeField] private GameObject meteorPrefab;
        [SerializeField] private GameObject[] spawnPoints;
        [SerializeField] private float spawnDuration;

        private float _durationManager;
        private float _time = 0f;

        private void Update() {
            _time += Time.deltaTime;
            if (_time > _durationManager) {
                _durationManager += spawnDuration;
                Instantiate(meteorPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].transform);
            }
        }
    }
}