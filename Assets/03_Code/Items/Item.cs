using UnityEngine;

namespace _03_Code.Items {
    public abstract class Item : MonoBehaviour, IItem {
        public virtual void Use(ItemUsingContext context) { }

        public virtual void HoldItem(ItemUsingContext context) { }

        public virtual void ReleaseItem(ItemUsingContext context) { }
    }
}