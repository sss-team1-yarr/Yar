using System.Collections;
using _03_Code.Enemy.Boss.Skill;
using UnityEngine;

namespace _03_Code.Enemy.Boss {
    public class Boss : MonoBehaviour {
        [SerializeField] private MeteorSpawner meteorSpawner;
        [SerializeField] private float randomTime = 20f;
        [SerializeField] private FinalSkill boom;
        [SerializeField] private Rigidbody2D rb;
        
        private float _timer;
        private int _randomNumber;
        public bool Death { get; private set; } = false;
        
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

        public void Last() {
            Death = true;
            StartCoroutine(Boom());
        }

        private IEnumerator Boom() {
            boom.HugeBoom();
            yield return new WaitForSeconds(1f);
        }
    }
}