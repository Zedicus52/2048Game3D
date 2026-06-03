using System;

namespace Game.Managers
{
    public interface IInputManager
    {
        public event Action<float> HorizontalInputChanged;
        public event Action ShootTriggered;
    }
}

