using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "Cube Entry Settings", menuName = "Settings/Cube Entry Settings")]
    public class CubeEntrySettings : ScriptableObject
    {
        public int CubeValue => _cubeValue;
        public Material CubeMaterial => _cubeMaterial;

        [SerializeField] private int _cubeValue;
        [SerializeField] private Material _cubeMaterial;
    }
}

