using UnityEngine;


[CreateAssetMenu(fileName = "new DefaultItem", menuName = "Item/Default")]
public class DefaultItemSO : ItemSO
{
    public override IItem GetItemInstance()
    {
        return Instantiate(prefab);
    }
}
