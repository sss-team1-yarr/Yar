using _03_Code.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _03_Code.Exp {
    public class ExpDrop : MonoBehaviour {
        [SerializeField] private Exp expPrefab;
        [SerializeField] private float minOffset = -1;
        [SerializeField] private float maxOffset = 1;

        private int _expCount;

        private void Start() {
            GameManager.Instance.dropManager.OnItemDrop += DropExp;
        }

        private void OnDisable() {
            GameManager.Instance.dropManager.OnItemDrop -= DropExp;
        }

        public void DropExp(GameObject target) {
            _expCount = GameManager.Instance.dropManager.expDropCount;
            for (var i = 0; i < _expCount; i++) {
                var exp = Instantiate(expPrefab, target.transform.position, Quaternion.identity);
                SetPosition(exp);
                exp.gameObject.SetActive(true);
            }
        }

        private void SetPosition(Exp exp) {
            var x = exp.transform.position.x + Random.Range(minOffset, maxOffset);
            var y = exp.transform.position.y + Random.Range(minOffset, maxOffset);
            exp.transform.position = new Vector3(x, y, exp.transform.position.z);
        }
    }
}