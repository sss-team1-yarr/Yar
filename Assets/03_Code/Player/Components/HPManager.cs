using TMPro;
using UnityEngine;

namespace _03_Code.Player.Components {
    public class HpManager : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private int hp = 10;

        private void Reset() {
            text = GameObject.Find("HP").GetComponent<TextMeshProUGUI>();
        }

        private void Start() {
            text.SetText($"HP: {hp}");
        }

        public void UpdateHp(int damage) {
            if (hp - damage <= 0) {
                hp = 0;
                text.SetText("HP: 0");
                GameManager.Instance.player.HandlePlayerDeath();
            } else {
                hp -= damage;
            }

            text.SetText($"HP: {hp}");
        }
    }
}