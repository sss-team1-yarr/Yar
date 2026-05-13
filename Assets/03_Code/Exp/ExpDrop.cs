using UnityEngine;

namespace _03_Code.Exp
{
    public class ExpDrop : MonoBehaviour
    {
        [SerializeField] private GameObject expPrefab;
        [SerializeField] private GameObject enemy;
        [SerializeField] private int expCount;
        
        private void DropExp()
        {
            if (enemy)
            {
                
                for(int i = 0; i <= expCount; i++)
                {
                    Instantiate(expPrefab, transform.position, transform.rotation);
                } 
            }
        }
    }
}
