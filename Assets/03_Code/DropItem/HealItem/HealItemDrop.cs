using _03_Code.Manager;
using UnityEngine;

namespace _03_Code.DropItem.HealItem {
    public class HealItemDrop : MonoBehaviour {
        [SerializeField] private GameObject healItemPrefab;


        private void Start() {
            GameManager.Instance.dropManager.OnItemDrop += DropHealItem;
        }

        private void OnDisable() {
            GameManager.Instance.dropManager.OnItemDrop -= DropHealItem;
        }

        public void DropHealItem(GameObject target) {
            var healItem = Instantiate(healItemPrefab, target.transform.position, Quaternion.identity);
            healItem.SetActive(true);
        }
    }
}