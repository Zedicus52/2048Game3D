using UnityEngine;

public class PlayerCubesPool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private bool _autoExpand = true;
    [SerializeField] private int _basicCount = 20;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private MenuManager _menuManager;
    
    private PoolBasic<Cube> _cubes;
    private CubeMovement _cubeMovement;
    private CubeDecorator _cubeDecorator;

    private void Awake()
    {
        _cubeMovement = GetComponent<CubeMovement>();
        _cubeDecorator = GetComponent<CubeDecorator>();
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
        _cubeDecorator.SetCubeColor(cube);
        cube.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        cube.gameObject.SetActive(true);
        cube.AddScore += _scoreManager.AddScore;
        cube.FinishGame += _menuManager.FinisGame;
        cube.FinishGame += _scoreManager.FinisGame;
        if(cube.gameObject.TryGetComponent(out Rigidbody rb))
            _cubeMovement.SetCubeRigidbody(rb);
    }
}
