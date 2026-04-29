using _03_Code.Handler;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _03_Code.UI.Pause {
    public class PauseUI : MonoBehaviour {
        [SerializeField] private GameObject settingsUI;
        [SerializeField] private EscHandler escHandler;
        
        
        private void Start() {
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            Time.timeScale = 0f;
        }

        public void ReturnGame() {
            escHandler.isActived = !escHandler.isActived;
            gameObject.SetActive(false);
        }

        public void Settings() {
            escHandler.isActived = !escHandler.isActived;
            gameObject.SetActive(false);
            settingsUI.SetActive(true);
        }

        private void OnDisable() {
            Time.timeScale = 1f;
        }

        public void Exit() {
            SceneManager.LoadScene(0);
        }
    }
}