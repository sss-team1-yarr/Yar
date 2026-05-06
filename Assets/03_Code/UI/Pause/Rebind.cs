using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Pause {
    public class Rebind : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI tmPro;
        [SerializeField] private Revive revive;
        [SerializeField] private string keyName;
        
        private InputActionRebindingExtensions.RebindingOperation _reBinding;
        
        
        private Controls _controls;
        
        private void Awake() {
            _controls = new Controls();
            UpdateKey();
        }

        public void KeyRebinding() {
            ReBindKey();
            UpdateKey();
        }
        
        public void ReBindKey() {
            revive.gameObject.SetActive(true);
            InputAction rebin = null;
            _controls.Disable();
            switch (keyName) {
                case "Jump":
                    rebin = _controls.Player.Jump;
                    break;
            }

            _reBinding = rebin.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => {
                    operation.Dispose();
                    _controls.Enable();
                    revive.gameObject.SetActive(false);
                })
                .Start();
        }

        public void UpdateKey() {
            InputAction update;
            string value = "";
            switch (keyName) {
                case "Jump":
                    update = _controls.Player.Jump;
                    value = update.GetBindingDisplayString(0);
                    break;
            }
            tmPro.SetText(value);
        }
    }
}