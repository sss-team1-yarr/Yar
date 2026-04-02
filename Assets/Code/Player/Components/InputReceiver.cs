using System;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void OnJump();
public delegate void OnMove();

namespace Code.Player.Components
{
    public class InputReceiver : MonoBehaviour, Controls.IPlayerActions
    {
        private Controls _controls;
        public event Action OnJumpInput;
        public event Action<float> OnMoveInput;

        private void Awake()
        {
            _controls = new Controls();
            _controls.Player.Enable();
            _controls.Player.SetCallbacks(this);
        }

        private void OnDestroy()
        {
            _controls.Disable();
            _controls.Dispose();
            _controls = null;
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            OnMoveInput?.Invoke(context.ReadValue<Vector2>().x);
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnJumpInput?.Invoke();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            
        }
    }
}