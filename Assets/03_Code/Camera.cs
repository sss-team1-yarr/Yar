using UnityEngine;

namespace _03_Code {
    public class Camera : MonoBehaviour {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }
    }
}