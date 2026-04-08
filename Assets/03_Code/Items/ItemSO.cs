using UnityEngine;

namespace _03_Code.Items {
    public abstract class ItemSO : ScriptableObject {
        [field: SerializeField] public string ItemName { get; private set; }
        [field: SerializeField] public Sprite Image { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [SerializeField] protected Item prefab;

        public abstract IItem GetItemInstance();
    }
}