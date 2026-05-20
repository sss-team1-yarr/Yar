using UnityEngine;

namespace _03_Code.Enemy.Common {
    public class ShowHp : MonoBehaviour {
        [SerializeField] private LineRenderer hpBar;
        [SerializeField] private Monster mob;


        public void UpdateHp(float hp) {
            hpBar.SetPosition(1, new Vector2(hp / mob.MaxHP - 0.5f, 0));
        }
    }
}