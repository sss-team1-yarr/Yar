using UnityEngine;

namespace _03_Code.Enemy.Boss.Components {
    public class BossHpManager : MonoBehaviour {
        [SerializeField] private int bossHealth;
        [SerializeField] private int bossPhaseTwoHealth;
        [SerializeField] private Boss owner;

        public int PhaseOneHealth { get; private set; }
        public int PhaseTwoHealth { get; private set; }

        private void Awake() {
            InitHealth();
        }

        private void InitHealth() {
            PhaseOneHealth = bossHealth;
            PhaseTwoHealth = bossPhaseTwoHealth;
        }

        public void Damage(int damage) {
            if (!owner.IsPhaseTwo)
                PhaseOneHealth -= damage;
            else
                PhaseTwoHealth -= damage;

            if (PhaseOneHealth <= 0) {
                PhaseOneHealth = 0;
                owner.StartPhaseTwo();
                PhaseTwoHealth = int.MaxValue;//다신 호출 불가능하게 만드는 장치
            }
        }
    }
}