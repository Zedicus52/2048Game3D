using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Managers
{
    public sealed class PCInputManager : BaseInputManager
    {
        [SerializeField] private InputActionReference _moveActionReference;
        [SerializeField] private InputActionReference _shootActionReference;

        private InputAction _moveAction;
        private InputAction _shootAction;


        private void Awake()
        {
            _moveAction = _moveActionReference.action;
            _shootAction = _shootActionReference.action;
        }

        private void OnEnable()
        {
            _moveAction.Enable();

            _moveAction.performed += OnMovePerformed;
            _moveAction.canceled += OnMoveCanceled;

            _shootAction.performed += OnShootActionPerformed;
        }

        private void OnDisable()
        {
            _moveAction.performed -= OnMovePerformed;
            _moveAction.canceled -= OnMoveCanceled;

            _shootAction.performed -= OnShootActionPerformed;

            _moveAction.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            OnHorizontalInputChanged(ctx.ReadValue<float>());
        }

        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            OnHorizontalInputChanged(0);
        }

        private void OnShootActionPerformed(InputAction.CallbackContext ctx)
        {
            OnShootTriggered();
        }
    }
}

