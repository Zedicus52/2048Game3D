using System;
using UnityEngine;
namespace Game.Managers
{
    public abstract class BaseInputManager : MonoBehaviour, IInputManager
    {
        public event Action<float> HorizontalInputChanged;
        public event Action ShootTriggered;

        protected void OnHorizontalInputChanged(float value)
        {
            HorizontalInputChanged?.Invoke(value);
        }

        protected void OnShootTriggered()
        {
            ShootTriggered?.Invoke();
        }
    }
}

