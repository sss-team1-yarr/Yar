using UnityEngine;
using UnityEngine.SceneManagement;

namespace _03_Code.DeadZone {
    public class DeadZone : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.tag == "Player")
                SceneManager.LoadScene(1);
            else
                Destroy(collision.gameObject);
        }
    }
}