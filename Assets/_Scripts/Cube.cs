using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    public event Func<int, Material> ChangeMaterial;
    public event Action<int> AddScore;
    public event Action<bool> WinGame;
    private int _number;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rb;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(transform.position.y < -50)
            gameObject.SetActive(false);
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
            if (cube._number == _number && cube._number != 2048)
            {
                AddScore?.Invoke(_number*2);
                Material mat = ChangeMaterial?.Invoke(_number * 2);
                cube.SetNumberAndColor(_number*2,mat);
                cube.AddImpulse();
                if (cube._number == 2048)
                    EndGame(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void EndGame(bool isWin)
    {
        WinGame?.Invoke(isWin);
    }

    private void AddImpulse()
    {
        float forceX = Random.Range(-3.2f, 3.2f);
        float forceY = Random.Range(1.5f, 3.2f);
        float forceZ = Random.Range(1.5f, 3.2f);
        _rb.AddForce(new Vector3(forceX,forceY,forceZ),ForceMode.Impulse);
    }
}
