using _03_Code.Player.Input;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.UI.Pause {
    public class Rebind : MonoBehaviour {
        [SerializeField] private string currentAction;

        [SerializeField] private GameObject selectedMarkObject;

        [SerializeField] private TextMeshProUGUI tmPro;

        [SerializeField] private int bindingIndex;

        private InputActionRebindingExtensions.RebindingOperation _reBinding;

        private InputAction _target;

        private void Start() {
            switch (currentAction) {
                case "Move":
                    _target = InputReceiver.Controls.Player.Move;
                    break;
                case "Jump":
                    _target = InputReceiver.Controls.Player.Jump;
                    break;
                case "Run":
                    _target = InputReceiver.Controls.Player.Run;
                    break;
                case "Skill1":
                    _target = InputReceiver.Controls.Player.Skill1;
                    break;
                case "Dash":
                    _target = InputReceiver.Controls.Player.Skill2;
                    break;
                case "Skill3":
                    _target = InputReceiver.Controls.Player.Skill3;
                    break;
                case "Attack":
                    _target = InputReceiver.Controls.Player.Attack;
                    break;
            }

            UpdateBind();
        }

        public void KeyRebinding() {
            selectedMarkObject.SetActive(true);

            _target.Disable();

            _reBinding = _target.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .WithTargetBinding(bindingIndex)
                .OnComplete(_ => {
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