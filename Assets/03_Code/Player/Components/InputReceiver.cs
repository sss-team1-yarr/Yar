using System;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnMove();

public delegate void OnJump();

namespace _3_Code.Player.Components {
    public class InputReceiver : MonoBehaviour, Controls.IPlayerActions {
        private Controls _controls;
        public event Action<float> OnMoveInput;
        public event Action OnJumpInput;

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


        public void OnAttack(InputAction.CallbackContext context) { }


        public void OnRun(InputAction.CallbackContext context) { }

        private void OnDestroy() {
            _controls.Disable();
            _controls.Dispose();
            _controls = null;
        }
    }
}