using System;
using System.Collections;
using _03_Code.Enemy.Boss.Components;
using _03_Code.Enemy.Boss.Skill;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _03_Code.Enemy.Boss {
    public enum SkillType {
        None,
        Meteor,
        HandAttack
    }
    
    public class Boss : MonoBehaviour {
        [SerializeField] private MeteorSpawner meteorSpawner;
        [SerializeField] private HandSpawner handSpawner;
        [SerializeField] private DropSkill dropSkill;
        
        [SerializeField] private float randomTime = 5f;
        [SerializeField] private FinalSkill boom;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private BossHpManager hp;
        [field: SerializeField] public float Speed { get; private set; } = 1f;
        [field: SerializeField] public float DetectRange { get; private set; } = 1f;

        private int _randomNumber;
        
        private SkillType _currentSkill;

        private float _timer;
        public bool Death { get; private set; }
        public bool IsPhaseTwo { get; private set; }

        private void Start() {
            StartCoroutine(SkillPhase());
        }

        private void Update() {
            _timer += Time.deltaTime;
            if (_timer >= randomTime) {
                if (_currentSkill == SkillType.Meteor) {
                    meteorSpawner.SpawnMeteor();
                }

                if (_currentSkill == SkillType.HandAttack && handSpawner.canSpawn) {
                    handSpawner.SpawnHand();
                }
            }
        }

        private IEnumerator SkillPhase() {
            while (!IsPhaseTwo) {
                _currentSkill = SkillType.Meteor;
                if (IsPhaseTwo) break;
                yield return new WaitForSeconds(15f);
                if (IsPhaseTwo) break;
                _currentSkill += 1;
                yield return new WaitForSeconds(10f);
                if (IsPhaseTwo) break;
                _currentSkill -= 1;
                yield return new WaitForSeconds(10f);
            }

            _currentSkill = SkillType.None;
            dropSkill.StartAttack();
            yield return new WaitForSeconds(24.6f);

            
        }

        public void StartPhaseTwo() {
            IsPhaseTwo = true;
            StartCoroutine(PhaseTwoStarted());
            Debug.Log("Starting PhaseTwo");
        }

        public void Last() {
            Death = true;
            StartCoroutine(Boom());
        }

        private IEnumerator PhaseTwoStarted() {
            yield return new WaitForSeconds(3f);
        }

        private IEnumerator Boom() {
            boom.HugeBoom();
            yield return new WaitForSeconds(1f);
        }
    }
}