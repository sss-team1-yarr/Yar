using _03_Code.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace _03_Code.Player.Components {
    public class HpManager : MonoBehaviour {
        [SerializeField] private Slider hpBar;
        [SerializeField] private int maxHp = 100;

        private int _hp;

        public static HpManager Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            _hp = maxHp;
            hpBar.value = maxHp / 100f;
        }

        public void Damage(int damage) {
            if (_hp - damage <= 0) {
                _hp = 0;

                GameManager.Instance.player.HandlePlayerDeath();
            } else {
                _hp -= damage;
            }

            hpBar.value = (float)_hp / maxHp;
        }

        public void Heal(int heal) {
            if (_hp + heal <= maxHp) return;
            _hp += heal;
            hpBar.value = (float)_hp / maxHp;
        }
    }
}