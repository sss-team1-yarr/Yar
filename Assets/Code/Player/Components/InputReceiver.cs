using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player.Components
{
    public class InputReceiver : MonoBehaviour, Controls.IPlayerActions
    {
        public void OnMove(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}