using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Pause {
    public class Settings : MonoBehaviour {
        [SerializeField] private GameObject pauseUI;

        private void Start() {
            gameObject.SetActive(false);
        }

        private void Update() {
            if (gameObject.activeSelf) {
                if (Keyboard.current.escapeKey.wasPressedThisFrame) {
                    pauseUI.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
        
        public void BackToMenu() {
            gameObject.SetActive(false);
            pauseUI.SetActive(true);
        }
        
        private void OnEnable() {
            Time.timeScale = 0f;
        }
    }
}