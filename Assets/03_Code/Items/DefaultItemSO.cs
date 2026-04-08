using UnityEngine;

namespace _03_Code.Items {
    [CreateAssetMenu(fileName = "new DefaultItem", menuName = "Item/Default")]
    public class DefaultItemSO : ItemSO
    {
        public override IItem GetItemInstance()
        {
            return Instantiate(prefab);
        }
    }
}
