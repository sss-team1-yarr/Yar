using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Pause {
    public class Rebind : MonoBehaviour {
        [SerializeField]
        private InputActionReference currentAction;
        [SerializeField]
        private GameObject selectedMarkObject;
        
        [SerializeField] private TextMeshProUGUI tmPro;
        
        private InputActionRebindingExtensions.RebindingOperation _reBinding;

        private void OnEnable() {
            UpdateBind();
        }

        public void KeyRebinding() {
            selectedMarkObject.SetActive(true);
            currentAction.action.Disable();
            
            _reBinding = currentAction.action.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .WithTargetBinding(0)
                .OnComplete(_=>Complete())
                .Start();
        }

        private void Complete() {
            selectedMarkObject.SetActive(false);
            _reBinding.Dispose();
            currentAction.action.Enable();
            UpdateBind();
        }

        private void UpdateBind() {
            tmPro.text = currentAction.action.GetBindingDisplayString(0);
        }
    }
}