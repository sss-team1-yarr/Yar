using UnityEngine;
using UnityEngine.SceneManagement;

namespace _03_Code.DeadZone {
    public class DeadZone : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player"))
                SceneManager.LoadScene(2);
            else
                Destroy(collision.gameObject);
        }
    }
}