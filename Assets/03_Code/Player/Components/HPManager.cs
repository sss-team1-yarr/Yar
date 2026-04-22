using TMPro;
using UnityEngine;

namespace _03_Code.Player.Components {
    public class HpManager : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI text;

        public int hp = int.MaxValue;

        public void UpdateHp(int min) {
            hp -= min;
            text.SetText($"HP: {hp}");
        }
    }
}