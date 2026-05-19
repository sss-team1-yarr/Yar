using System;
using UnityEngine;

namespace _03_Code.Exp
{
    public class DropManager : MonoBehaviour
    {
        [SerializeField] private int expCount;
        [SerializeField] private float maxOffset;
        [SerializeField] private float minOffset;

        public int expDropCount;

        public event Action<GameObject> OnItemDrop;

        public void DropItem(GameObject target, int value) {
            expDropCount = value;
            OnItemDrop?.Invoke(target);
        }
    }
}
