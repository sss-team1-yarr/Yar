using _03_Code.Handler;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Pause {
    public class Settings : MonoBehaviour {
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private EscHandler escHandler;
        
        private void Start() {
            gameObject.SetActive(false);
        }

        private void Update() {
            if (gameObject.activeSelf) {
                if (Keyboard.current.escapeKey.wasPressedThisFrame) {
                    escHandler.isActived = true;
                    pauseUI.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
        
        public void BackToMenu() {
            escHandler.isActived = !escHandler.isActived;
            gameObject.SetActive(false);
            pauseUI.SetActive(true);
        }
        
        private void OnEnable() {
            Time.timeScale = 0f;
        }
    }
}