using UnityEngine;
using UnityEngine.SceneManagement;

namespace _03_Code.UI.Title {
    public class MainMenu : MonoBehaviour {
        [SerializeField] private GameObject settings;


        public void GameStart() {
            SceneManager.LoadScene(1);
        }

        public void SettingsMenu() {
            settings.SetActive(true);
        }

        public void ExitGame() {
            Application.Quit();
        }
    }
}