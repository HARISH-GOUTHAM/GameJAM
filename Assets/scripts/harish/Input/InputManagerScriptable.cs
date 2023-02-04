using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    [CreateAssetMenu(fileName = "InputManagerScriptable", menuName = "Input", order = 0)]
    public class InputManagerScriptable : ScriptableObject,PlayerInputs.IPlayerControllerActions
    {

      
        public event UnityAction<Vector2> OnMouseMoveEv = delegate {  };
        public event UnityAction OnJumpEv = delegate {  };
        public event UnityAction<Vector2> OnMoveEv = delegate {  };
        public event UnityAction OnSwordAttackEv = delegate {  };
        public event UnityAction OnShootEv = delegate {  };
        public event UnityAction OnSuckEv = delegate {  };
        
        private PlayerInputs _playerInputs;

        private void OnEnable()
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Enable();
            _playerInputs.PlayerController.SetCallbacks(this);
        }

        public void OnCamLook(InputAction.CallbackContext context)
        {
            OnMouseMoveEv.Invoke(context.ReadValue<Vector2>());
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
                OnJumpEv.Invoke();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            OnMoveEv.Invoke(context.ReadValue<Vector2>());
        }

        public void OnSwordAttack(InputAction.CallbackContext context)
        {
            OnSwordAttackEv.Invoke();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            OnShootEv.Invoke();
        }

        public void OnSoulSuck(InputAction.CallbackContext context)
        {
            OnSuckEv.Invoke();
        }
    }
}