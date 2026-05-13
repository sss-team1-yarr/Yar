using UnityEngine;

namespace _03_Code.Exp
{
    public class ExpDropManager : MonoBehaviour
    {
        [SerializeField] private GameObject expPrefab;
        [SerializeField] private int expCount;

        public void DropExp(GameObject target) {
            GameObject exp;
            for (int i = 0; i < expCount; i++) { 
                exp = Instantiate(expPrefab, target.transform.position, Quaternion.identity);
                exp.SetActive(true);
            }
        }
    }
}
