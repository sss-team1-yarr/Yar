using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.Handler {
    public class EscHandler : MonoBehaviour {
        [SerializeField] private GameObject pause;
        
        private bool _isActive;
        
        private void Update() {
            Menu();
        }

        private void Menu() {
            if (!Keyboard.current.escapeKey.wasPressedThisFrame) return;
            switch (pause.activeSelf) {
                case false:
                    pause.SetActive(true);
                    EscPressed();
                    break;
                case true:
                    pause.SetActive(false);
                    EscPressed();
                    break;
            }
        }

        public void EscPressed() {
            _isActive = !_isActive;
        }
    }
}