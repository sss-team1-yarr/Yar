using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Title {
    public class Settings : MonoBehaviour {
        private void Start() {
            gameObject.SetActive(false);
        }

        private void Update() {
            if (gameObject.activeSelf)
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
                    gameObject.SetActive(false);
        }

        private void OnEnable() {
            Time.timeScale = 0f;
        }

        public void BackToMenu() {
            gameObject.SetActive(false);
        }
    }
}