using TMPro;
using UnityEngine;

namespace _03_Code.Enemy.Common
{
    public class ShowHp : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;

        private void Reset()
        {
            text = GetComponent<TextMeshPro>();
        }

        public void UpdateHp(int hp)
        {
            text.text = $"HP : {hp}";
        }
    }
}
