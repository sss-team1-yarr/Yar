using UnityEngine;

namespace _03_Code.Items {
    public interface IItem {
        bool CanUse { get; }
        Transform transform { get; }

        void Use(ItemUsingContext context);
        void HoldItem(ItemUsingContext context);
        void ReleaseItem(ItemUsingContext context);
    }

    public struct ItemUsingContext {
        public int Input;
        public bool Pressed;
        public Player.Main.Player User;
    }
}