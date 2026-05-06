using _03_Code.Player.Components;
using _03_Code.Player.Main;
using UnityEngine;

namespace _03_Code {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public PlayerSystem playerSystem;
        public PlayerHit playerHit;
    
        private void Awake() {
            Instance = this;
        }
    }
}
