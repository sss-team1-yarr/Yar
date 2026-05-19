using System;
using UnityEngine;
using UnityEngine.UI;

namespace _03_Code.Enemy.Boss.Components {
    public class BossHpManager : MonoBehaviour {
        [SerializeField] private int bossHealth;
        [SerializeField] private int bossPhaseTwoHealth;
        [SerializeField] private Boss owner;
        [SerializeField] private Slider hpBar;

        public int PhaseOneHealth { get; private set; }
        public int PhaseTwoHealth { get; private set; }

        private void Awake() {
            InitHealth();
        }

        private void Start()
        {
            hpBar.value = (float)PhaseOneHealth/bossHealth;
        }

        private void InitHealth() {
            PhaseOneHealth = bossHealth;
            PhaseTwoHealth = bossPhaseTwoHealth;
        }

        public void Damage(int damage) {
            if (!owner.IsPhaseTwo) {
                PhaseOneHealth -= damage;
                hpBar.value = (float)PhaseOneHealth / bossHealth;
            } else {
                PhaseTwoHealth -= damage;
                hpBar.value = (float)PhaseTwoHealth / bossHealth;
            }

            if (PhaseOneHealth <= 0) {
                PhaseOneHealth = 0;
                owner.StartPhaseTwo();
                PhaseOneHealth = int.MaxValue; //다신 호출 불가능하게 만드는 장치
            }
            if (PhaseTwoHealth <= 0) {
                PhaseOneHealth = 0;
                owner.Last();
                PhaseOneHealth = int.MaxValue; //다신 호출 불가능하게 만드는 장치
            }
        }
    }
}