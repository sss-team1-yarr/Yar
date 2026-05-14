using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _03_Code.Exp
{
    public class ExpDropManager : MonoBehaviour
    {
        [SerializeField] private GameObject expPrefab;
        [SerializeField] private int expCount;
        [SerializeField] private float maxOffset;
        [SerializeField] private float minOffset;

        private void Reset()
        {
            expPrefab = Resources.Load<GameObject>("Prefabs/Exp"); //rider Auto-Made
        }

        public void DropExp(GameObject target, int value) {
            GameObject exp;
            expCount = value;
            for (int i = 0; i < expCount; i++) { 
                exp = Instantiate(expPrefab, target.transform.position, Quaternion.identity);
                SetPosition(exp);
                exp.SetActive(true);
            }
        }

        private void SetPosition(GameObject exp) {
            float x = exp.transform.position.x + Random.Range(minOffset, maxOffset);
            float y = exp.transform.position.y + Random.Range(minOffset, maxOffset);
            exp.transform.position = new Vector3(x, y, exp.transform.position.z);
        }
    }
}
