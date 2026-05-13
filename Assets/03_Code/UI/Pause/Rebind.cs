using System;
using _03_Code.Player.Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Pause {
    public class Rebind : MonoBehaviour {
        [SerializeField]
        private string currentAction;
        [SerializeField]
        private GameObject selectedMarkObject;
        
        [SerializeField] private TextMeshProUGUI tmPro;
        
        [SerializeField] private int bindingIndex = 0;

        private InputActionRebindingExtensions.RebindingOperation _reBinding;
        
        private InputAction _target;

        private void Awake() {
            switch (currentAction) {
                case "Jump":
                    _target = InputReceiver.Controls.Player.Jump;
                    break;
            }
        }

        private void OnEnable() {
            UpdateBind();
        }

        public void KeyRebinding() {
            selectedMarkObject.SetActive(true);
            
            _target.Disable();
            
            _reBinding = _target.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .WithTargetBinding(bindingIndex)
                .OnComplete(_=> {
                        selectedMarkObject.SetActive(false);
                        _reBinding.Dispose();
                        _target.Enable();
                        UpdateBind();
                    }
                )
                .Start();
        }

        private void UpdateBind() {
            tmPro.text = _target.GetBindingDisplayString(0);
        }
    }
}