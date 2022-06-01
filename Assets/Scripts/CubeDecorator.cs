using System.Collections.Generic;
using UnityEngine;
public class CubeDecorator : MonoBehaviour
{
    [SerializeField] private List<CubeMaterial> _cubesMaterial;

    public void SetCubeColor(Cube cube)
    {
        int ind = Random.Range(0, _cubesMaterial.Count);
        cube.SetNumberAndColor(_cubesMaterial[ind].Number, _cubesMaterial[ind].CurrentColor);
    }

    
}
[System.Serializable]
public class CubeMaterial
{
    public int Number;
    public Material CurrentColor;
    public Material NextColor;
}
