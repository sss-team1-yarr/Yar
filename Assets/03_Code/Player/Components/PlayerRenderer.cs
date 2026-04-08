using UnityEngine;

namespace _03_Code.Player.Components {
    public class PlayerRenderer : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform playerTransform;

        public void SetFloatValue(int hash, float value)
        {
            animator.SetFloat(hash, value);
        }

        public void SetBoolValue(int hash, bool value)
        {
            animator.SetBool(hash, value); 
        }

        public void SetFlip(bool isRight) 
        {
            playerTransform.transform.eulerAngles = new Vector3(0f, isRight ? 0f : 180f, 0f);
        }
    }
}
