using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _03_Code.UI.Pause {
    public class PauseUI : MonoBehaviour {

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                gameObject.SetActive(true);
            }
        }

        public void ReturnGame()
        {
            gameObject.SetActive(true);
        }

        public void Exit()
        {
            SceneManager.LoadScene(0);
        }
    }
}