using UnityEngine;
using UnityEngine.SceneManagement;

namespace _03_Code.UI.Title {
    public class PressStart : MonoBehaviour {
        public void GameStart() {
            SceneManager.LoadScene(1);
        }

        public void ExitGame() {
            Application.Quit();
        }
    }
}