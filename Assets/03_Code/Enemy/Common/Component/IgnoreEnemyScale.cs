using System;
using UnityEngine;

namespace _03_Code.Enemy.Common.Component
{
    public class IgnoreEnemyScale : MonoBehaviour
    {
        //이 기능이 과연 쓸모가 있을까 싶어서 주석 처리   
        
        /*private void Start()
        {
            Vector3 parentScale = transform.parent.lossyScale;
            transform.localScale = new Vector3(1f / parentScale.x, 1f / parentScale.y, transform.localScale.z);
        }*/
    }
}
