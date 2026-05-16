using _03_Code.Enemy.Boss.Skill;
using UnityEngine;

namespace _03_Code.Enemy.Boss {
    public class Boss : MonoBehaviour {
        [SerializeField] private MeteorSpawner meteorSpawner;
        [SerializeField] private float randomTime = 20f;
        
        private float _timer;
        private int _randomNumber;
        
        private void Update() {
            _timer += Time.deltaTime;
            if (_timer >= randomTime) {
                _randomNumber = Random.Range(0,3);
                switch (_randomNumber) {
                    case 0:
                        meteorSpawner.SpawnMeteor();
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
        }
    }
}