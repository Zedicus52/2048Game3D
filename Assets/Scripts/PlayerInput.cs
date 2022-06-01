using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static event Action<float> OnMove;
    public static event Action EndMove;
    private Vector2 _startPos = Vector2.zero;
    private float _direction = 0f;

    private void Update()
    {
#if UNITY_EDITOR
        OnMove?.Invoke(Input.GetAxis("Horizontal"));
#endif
        
#if  UNITY_ANDROID
        GetTouchInput();
#endif
    }

    private void GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    if (touch.position.x > _startPos.x + 15) 
                    { _direction = 1f; }  
                    if(touch.position.x <= _startPos.x-15)  
                    { _direction = -1f; }
                    break;
                case TouchPhase.Ended:
                    EndMove?.Invoke();
                    break;
                default:
                    _startPos = touch.position;
                    _direction = 0f;
                    break;
            }
            OnMove?.Invoke(_direction);
        }
    }
}
