using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hyunwoo.CoreLib
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        public Controls Controls;

        public event Action OnUpKeyPressed;
        public event Action OnDownKeyPressed;
        public event Action OnLeftKeyPressed;
        public event Action OnRightKeyPressed;
        
        private void OnEnable()
        {
            if (Controls == null)
            {
                Controls = new Controls();
                Controls.Enable();
            }
            
            Controls.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            Controls.Disable();
        }

        public void OnUp(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnUpKeyPressed?.Invoke();
        }

        public void OnDown(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDownKeyPressed?.Invoke();
        }

        public void OnLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnLeftKeyPressed?.Invoke();
        }

        public void OnRight(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnRightKeyPressed?.Invoke();
        }
    }
}
