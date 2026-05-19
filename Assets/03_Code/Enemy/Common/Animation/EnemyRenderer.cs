using UnityEngine;

namespace _03_Code.Enemy.Common.Animation {
    public class EnemyRenderer : MonoBehaviour {
        [SerializeField] private Animator animator;

        public void SetFloatValue(int hash, float value) {
            animator.SetFloat(hash, value);
        }

        public void SetBoolValue(int hash, bool value) {
            animator.SetBool(hash, value);
        }
    }
}