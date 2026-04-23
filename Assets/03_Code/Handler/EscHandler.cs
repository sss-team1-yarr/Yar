using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.Handler {
    public class EscHandler : MonoBehaviour {
        [SerializeField] private GameObject pause;
        
        public bool isActived;
        
        private void Update() {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
                switch (isActived) {
                    case false:
                        pause.SetActive(true);
                        isActived = !isActived;
                        break;
                    case true:
                        pause.SetActive(false);
                        isActived = !isActived;
                        break;
                }
        }
    }
}