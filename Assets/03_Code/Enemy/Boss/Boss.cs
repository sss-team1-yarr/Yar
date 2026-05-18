using System.Collections;
using _03_Code.Enemy.Boss.Components;
using _03_Code.Enemy.Boss.Skill;
using UnityEngine;

namespace _03_Code.Enemy.Boss {
    public class Boss : MonoBehaviour {
        [SerializeField] private MeteorSpawner meteorSpawner;
        [SerializeField] private float randomTime = 20f;
        [SerializeField] private FinalSkill boom;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private BossHpManager hp;
        [field:SerializeField] public float Speed{get; private set;} = 1f;
        [field:SerializeField] public float DetectRange{get; private set;} = 1f;
        
        private int _randomNumber;

        private float _timer;
        public bool Death { get; private set; }
        public bool IsPhaseTwo { get; private set; }

        private void Update() {
            _timer += Time.deltaTime;
            if (_timer >= randomTime) {
                _randomNumber = Random.Range(0, 3);
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

        public void StartPhaseTwo() {
            IsPhaseTwo = true;
            StartCoroutine(PhaseTwoStarted());
        }

        public void Last() {
            Death = true;
            StartCoroutine(Boom());
        }

        private IEnumerator PhaseTwoStarted() {
            Debug.Log("Starting PhaseTwo");
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator Boom() {
            boom.HugeBoom();
            yield return new WaitForSeconds(1f);
        }
    }
}