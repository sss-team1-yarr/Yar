using UnityEngine;
using UnityEngine.SceneManagement;

namespace _03_Code.UI.Pause {
    public class PauseUI : MonoBehaviour {
        public void Restart()
        {
            SceneManager.LoadScene(1);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}