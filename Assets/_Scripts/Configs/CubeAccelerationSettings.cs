using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Cube Acceleration Settings", menuName = "Settings/Cube Acceleration Settings")]
    public sealed class CubeAccelerationSettings : ScriptableObject
    {
        public float AccelerationSpeed => _accelerationSpeed;
        public float Speed => _speed;

        [SerializeField] private float _accelerationSpeed;
        [SerializeField] private float _speed;
    }
}

