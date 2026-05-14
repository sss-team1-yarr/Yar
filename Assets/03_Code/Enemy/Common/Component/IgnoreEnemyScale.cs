using System;
using UnityEngine;

namespace _03_Code.Enemy.Common.Component
{
    public class IgnoreEnemyScale : MonoBehaviour
    {
        private void Start()
        {
            Vector3 parentScale = transform.parent.lossyScale;
            transform.localScale = new Vector3(1f / parentScale.x, 1f / parentScale.y, transform.localScale.z);
        }
    }
}
