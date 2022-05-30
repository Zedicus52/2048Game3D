using UnityEngine;

public class PlayerCubesPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private int _basicCount = 20;
    [SerializeField] private Vector3 _startPos;
    
    private PoolBasic<Cube> _cubes;
    private CubeMovement _cubeMovement;

    private void Awake()
    {
        _cubeMovement = GetComponent<CubeMovement>();
        _cubes = new PoolBasic<Cube>(_prefab,_autoExpand,_basicCount, transform);
        GetCube();
    }

    private void OnEnable()
    {
        _cubeMovement.SpawnCube += GetCube;
    }

    private void OnDisable()
    {
        _cubeMovement.SpawnCube -= GetCube;
    }

    private void GetCube()
    {
        var cube = _cubes.GetFreeElement();
        cube.transform.position = _startPos;
        cube.gameObject.SetActive(true);
        if(cube.gameObject.TryGetComponent(out Rigidbody rb))
            _cubeMovement.SetCubeRigidbody(rb);
    }
}
