using System.Collections;
using _03_Code.Items.Object.Button;
using UnityEngine;

namespace _03_Code.Items.Object {
    public class PlayerChecker : MonoBehaviour {
        [SerializeField] private OnButton onButton;

        private bool _isChecked;

        private void Reset() {
            onButton = GetComponentInParent<OnButton>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Debug.Log(collision);

            if (!collision.CompareTag("Player"))
                return;

            if (_isChecked)
                return;

            StartCoroutine(MakeEnemy());
        }

        private IEnumerator MakeEnemy() {
            _isChecked = true;
            yield return new WaitForSeconds(0.2f);

            onButton.PressedButton();
            var enemy = gameObject.transform.parent.gameObject;
            enemy.SetActive(false);
        }
    }
}