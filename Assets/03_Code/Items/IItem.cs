using Code.Player;
using UnityEngine;

public interface IItem 
{
    ItemSO Data { get; }
    bool CanUse { get; }
    Transform transform { get; } 

    void Use(ItemUsingContext context);
    void HoldItem(ItemUsingContext context);
    void ReleaseItem(ItemUsingContext context);
}

public struct ItemUsingContext 
{
    public int Input;
    public bool Pressed;
    public Player User;
}
