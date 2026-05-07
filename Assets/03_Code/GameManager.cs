using _03_Code.Enemy.Common;
using _03_Code.Player.Components;
using _03_Code.Player.Main;
using UnityEngine;

namespace _03_Code {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public Player.Main.Player player;
        public PlayerControl playerSystem;
        public PlayerHit playerHit;
        public HpManager hpManager;
    
        private void Awake() {
            Instance = this;
        }
    }
}
