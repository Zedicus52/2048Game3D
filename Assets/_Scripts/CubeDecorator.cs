using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class CubeDecorator : MonoBehaviour
{
    [SerializeField] private List<CubeMaterial> _cubesMaterial;
    

    public void SetCubeColor(Cube cube)
    {
        cube.ChangeMaterial -= GetMaterial;
        int ind = Random.Range(0, _cubesMaterial.Count);
        while (_cubesMaterial[ind].Number == 2048)
        {
            ind = Random.Range(0, _cubesMaterial.Count);
        }
        cube.SetNumberAndColor(_cubesMaterial[ind].Number, _cubesMaterial[ind].CurrentColor);
        cube.ChangeMaterial += GetMaterial;
    }

    private Material GetMaterial(int number)
    {
        return (from item in _cubesMaterial where item.Number == number select item.CurrentColor).FirstOrDefault();
    }

    
}
[System.Serializable]
public class CubeMaterial
{
    public int Number;
    public Material CurrentColor;
}
