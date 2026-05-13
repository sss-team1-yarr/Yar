using UnityEngine;

namespace _03_Code.Player.Components {
    public class ExpManager : MonoBehaviour {
        [SerializeField] private int expGauge = 0;
        [SerializeField] private int expLevel = 1;
        

        public void ExpAdd() {
            if (expGauge + 1 >= 100) {
                ExpLevelUp();
            }
            else {
                expGauge += 1;
            }
        }
        
        private void ExpLevelUp() {
            expLevel++;
            expGauge = 0;
        }
    }
}