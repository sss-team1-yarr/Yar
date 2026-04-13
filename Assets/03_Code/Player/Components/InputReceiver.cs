using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _3_Code.Player.Components {
    public class InputReceiver : MonoBehaviour, Controls.IPlayerActions {
        private Controls _controls;
        public event Action<int, bool> OnAttackInput;
        public event Action<float> OnMoveInput;
        public event Action OnJumpInput;
        public event Action OnRunInput;
        public event Action OnSkill1Input;
        public event Action OnPauseInput;

        private void Awake() {
            _controls = new Controls();
            _controls.Player.Enable();
            _controls.Player.SetCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context) {
            OnMoveInput?.Invoke(context.ReadValue<Vector2>().x);
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (context.performed)
                OnJumpInput?.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.performed)
                OnAttackInput?.Invoke(0, true);
            if (context.canceled)
                OnAttackInput?.Invoke(0, false);
        }

        public void OnRun(InputAction.CallbackContext context) {
            if (context.performed)
                OnRunInput?.Invoke();
        }
        public void OnSkill1(InputAction.CallbackContext context) {
            if (context.performed)
                OnSkill1Input?.Invoke();
        }
        public void OnSkill2(InputAction.CallbackContext context) { }
        public void OnPause(InputAction.CallbackContext context) {
            if (context.performed) {
                OnPauseInput?.Invoke();
            }
        }

        private void OnDestroy() {
            _controls.Disable();
            _controls.Dispose();
            _controls = null;
        }
    }
}