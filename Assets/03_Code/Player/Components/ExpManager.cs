using UnityEngine;

namespace _03_Code.Player.Components {
    public class ExpManager : MonoBehaviour {
        [SerializeField] private int expGauge;
        [SerializeField] private int expLevel = 1;


        public void ExpAdd() {
            if (expGauge + 5 >= 100)
                ExpLevelUp();
            else
                expGauge += 5;
        }

        private void ExpLevelUp() {
            expLevel++;
            expGauge = 0;
        }
    }
}