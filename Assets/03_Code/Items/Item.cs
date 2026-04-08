using UnityEngine;

namespace _03_Code.Items {
    public abstract class Item : MonoBehaviour, IItem {
        [field: SerializeField] public ItemSO Data { get; private set; }
        public abstract bool CanUse { get; }


        public virtual void Use(ItemUsingContext context) { }

        public virtual void HoldItem(ItemUsingContext context) { }

        public virtual void ReleaseItem(ItemUsingContext context) { }
    }
}