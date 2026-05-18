using System;
using UnityEngine;
using UnityEngine.UI;

namespace _03_Code.Player.Components {
    public class ExpManager : MonoBehaviour {
        [SerializeField] private int expGauge;
        [SerializeField] private int expLevel = 1;
        [SerializeField] private Slider expBar;

        private void Start() {
            expBar.maxValue = expGauge/100f;
        }

        public void ExpAdd() {
            if (expGauge + 5 >= 100)
                ExpLevelUp();
            else
                expGauge += 5;
            expBar.value = expGauge/100f;
        }

        private void ExpLevelUp() {
            expLevel++;
            expGauge = 0;
        }
    }
}