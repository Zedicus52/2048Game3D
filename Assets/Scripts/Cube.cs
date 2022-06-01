using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Func<int, Material> ChangeMaterial;
    public int Number => _number;
    private int _number;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetNumberAndColor(int num, Material mat)
    {
        _number = num;
        _meshRenderer.material = mat;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            if (cube.Number == _number && cube.Number != 2048)
            {
                Material mat = ChangeMaterial?.Invoke(_number * 2);
                cube.SetNumberAndColor(_number*2,mat);
                gameObject.SetActive(false);
            }
        }
    }
}
