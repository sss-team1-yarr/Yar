using _03_Code.Player.Main;
using UnityEngine;

namespace _03_Code {
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public PlayerMove playerMove; 
    
        private void Awake() {
            Instance = this;
        }
    }
}
