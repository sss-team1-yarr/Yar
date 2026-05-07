using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Pause {
    public class Rebind : MonoBehaviour {
        [SerializeField]
        private InputActionReference currentAction = null;
        [SerializeField]
        private GameObject selectedMarkObject;
        [SerializeField]
        private InputBinding.DisplayStringOptions displayStringOptions;
        
        [SerializeField] private TextMeshProUGUI tmPro;
        
        private InputActionRebindingExtensions.RebindingOperation _reBinding;
        
        
        private Controls _controls;
        
        private void Awake() {
            _controls = new Controls();
        }

        private void OnEnable() {
            UpdateBind();
        }

        public void KeyRebinding() {
            ReBindKey();
        }
        
        public void ReBindKey() {
            currentAction.action.Disable();
            selectedMarkObject.SetActive(true);
            
            _reBinding = currentAction.action.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .OnComplete(operation=>Complete())
                .Start();
        }

        private void Complete() {
            selectedMarkObject.SetActive(false);
            _reBinding.Dispose();
            currentAction.action.Enable();
            
        }

        private void UpdateBind() {
            var displayStr = string.Empty;
            var deviceLayoutName = default(string);
            var controlPath = default(string);
            
            displayStr = currentAction.action.GetBindingDisplayString(0, out deviceLayoutName, out controlPath, displayStringOptions);
            
            tmPro.text = displayStr;
        }
    }
}