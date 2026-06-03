using System.Collections.Generic;
using UnityEngine;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "CubeEntryStore", menuName = "Settings/CubeEntryStore")]
    public class CubeEntryStore : ScriptableObject
    {
        public IReadOnlyCollection<CubeEntrySettings> CubeSettigs => _cubeSettings;
        public CubeEntrySettings MaxCubeEntrySettings => _maxCubeEntry;

        [SerializeField] private List<CubeEntrySettings> _cubeSettings;
        [SerializeField] private CubeEntrySettings _maxCubeEntry;
    }
}