using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Settings {
    public class Settings : MonoBehaviour {
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private Player.Main.Player player;
        
        private void Start() {
            gameObject.SetActive(false);
        }

        private void Update() {
            if (gameObject.activeSelf) {
                if (Keyboard.current.escapeKey.wasPressedThisFrame) {
                    player.isActived = true;
                    pauseUI.SetActive(true);
                    gameObject.SetActive(false);
                }
            }
        }
        
        public void BackToMenu() {
            player.isActived = !player.isActived;
            gameObject.SetActive(false);
            pauseUI.SetActive(true);
        }
        
        private void OnEnable() {
            Time.timeScale = 0f;
        }
    }
}