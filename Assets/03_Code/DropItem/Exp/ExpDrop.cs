using System;
using _03_Code.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _03_Code.Exp {
    public class ExpDrop : MonoBehaviour {
        [SerializeField] private GameObject expPrefab;
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
            for (int i = 0; i < _expCount; i++) { 
                GameObject exp = Instantiate(expPrefab, target.transform.position, Quaternion.identity);
                SetPosition(exp);
                exp.SetActive(true);
            }
        }

        private void SetPosition(GameObject exp) {
            float x = exp.transform.position.x + Random.Range(minOffset, maxOffset);
            float y = exp.transform.position.y + Random.Range(minOffset, maxOffset);
            exp.transform.position = new Vector3(x, y, exp.transform.position.z);
        }
    }
}