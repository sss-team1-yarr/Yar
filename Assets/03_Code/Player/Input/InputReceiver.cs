using System;
using _03_Code.Player.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _03_Code.Player.Input {
    public class InputReceiver : MonoBehaviour, Controls.IPlayerActions, IPlayerModule {
        private Controls _controls;
        public event Action<int, bool> OnAttackInput;
        public event Action<float> OnMoveInput;
        public event Action OnJumpInput;
        public event Action<bool> OnRunInput;
        public event Action OnSkill1Input;
        public event Action OnSkill2Input;
        public event Action OnGuardInput;

        public Vector2 OnMoveInputVec2 { get; private set; }

        private void Awake() {
            _controls = new Controls();
            _controls.Player.Enable();
            _controls.Player.SetCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context) {
            OnMoveInputVec2 = context.ReadValue<Vector2>();
            OnMoveInput?.Invoke(OnMoveInputVec2.x);
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
                OnRunInput?.Invoke(true);
            if (context.canceled)
                OnRunInput?.Invoke(false);
        }

        public void OnSkill1(InputAction.CallbackContext context) {
            if (context.performed)
                OnSkill1Input?.Invoke();
        }

        public void OnSkill2(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSkill2Input?.Invoke();
        }

        public void OnGuard(InputAction.CallbackContext context) {
            OnGuardInput?.Invoke();
        }

        private void OnDestroy() {
            _controls.Disable();
            _controls.Dispose();
            _controls = null;
        }

        public void Initialize(Main.Player owner) { }
    }
}