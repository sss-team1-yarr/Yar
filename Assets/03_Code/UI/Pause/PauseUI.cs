using UnityEngine;
using UnityEngine.SceneManagement;

namespace _03_Code.UI.Pause {
    public class PauseUI : MonoBehaviour {
        [SerializeField] private GameObject settingsUI;
        [SerializeField] private Player.Main.Player player;
        
        
        private void Start() {
            gameObject.SetActive(false);
        }

        private void OnEnable() {
            Time.timeScale = 0f;
        }

        public void ReturnGame() {
            gameObject.SetActive(false);
        }

        public void Settings() {
            player.isActived = !player.isActived;
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