using UnityEngine;
using UnityEngine.InputSystem;
namespace Game.Managers
{
    public sealed class MobileInputManager : BaseInputManager, IInputManager
    {
        [SerializeField] private InputActionReference _touchPositionActionReference;
        [SerializeField] private InputActionReference _touchContactActionReference;

        private InputAction _touchPositionAction;
        private InputAction _touchContactAction;

        private Vector2 _startPos;
        private float _direction;
        private bool _isTouching;


        private void Awake()
        {
            _touchPositionAction = _touchPositionActionReference.action;
            _touchContactAction = _touchContactActionReference.action;
        }

        private void OnEnable()
        {
            _touchPositionAction.Enable();
            _touchContactAction.Enable();

            _touchContactAction.started += OnTouchStarted;
            _touchContactAction.canceled += OnTouchEnded;
            _touchPositionAction.performed += OnTouchMoved;
        }

        private void OnDisable()
        {
            _touchContactAction.started -= OnTouchStarted;
            _touchContactAction.canceled -= OnTouchEnded;
            _touchPositionAction.performed -= OnTouchMoved;

            _touchPositionAction.Disable();
            _touchContactAction.Disable();
        }

        private void OnTouchMoved(InputAction.CallbackContext ctx)
        {
            if (!_isTouching) 
                return;

            Vector2 currentPos = ctx.ReadValue<Vector2>();

            if (currentPos.x > _startPos.x + 15f)
                _direction = 1f;
            else if (currentPos.x <= _startPos.x - 15f)
                _direction = -1f;

            OnHorizontalInputChanged(_direction);
        }

        private void OnTouchStarted(InputAction.CallbackContext ctx)
        {
            _startPos = _touchPositionAction.ReadValue<Vector2>();
            _direction = 0f;
            _isTouching = true;
        }

        private void OnTouchEnded(InputAction.CallbackContext ctx)
        {
            _isTouching = false;
            OnShootTriggered();
        }
    }
}

