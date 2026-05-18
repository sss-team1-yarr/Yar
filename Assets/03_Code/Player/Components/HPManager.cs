using System;
using UnityEngine;
using UnityEngine.UI;

namespace _03_Code.Player.Components {
    public class HpManager : MonoBehaviour {
        [SerializeField] private Slider hpBar;
        [SerializeField] private int maxHp = 100;
        
        private int _hp;

        private void Start() {
            _hp = maxHp;
        }

        public void UpdateHp(int damage) {
            if (_hp - damage <= 0) {
                _hp = 0;
                
                GameManager.Instance.player.HandlePlayerDeath();
            } else {
                _hp -= damage;
            }
            hpBar.value = (float)_hp / maxHp;
        }
    }
}