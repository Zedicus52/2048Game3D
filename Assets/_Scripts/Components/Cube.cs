using Game.Settings;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public sealed class Cube : MonoBehaviour
{
    private const float kPostMovementDelay = 0.5f;
    private const float kMaxImpulseForce = 3.2f;
    private const float kMinImpulseForce = 1.5f;

    public event Action<Cube, Cube> CubesCollides;
    public event Action CubeLauched;

    public Rigidbody Rigidbody => _rb;
    public int CubeValue { get; private set; }

    [SerializeField] private TMP_Text[] _cubeNumber;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rb;
    private Transform _transform;
    private GameObject _gameObject;
    private Coroutine _impulseRoutine;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
        _gameObject = gameObject;
    }

    private void Update()
    {
        if (_transform.position.y < -50)
            _gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cube cube))
        {
            OnCubeCollides(this, cube);
            OnCubeLauched();
        }
    }

    public void SetCubeSettings(CubeEntrySettings settings)
    {
        CubeValue = settings.CubeValue;
        _meshRenderer.sharedMaterial = settings.CubeMaterial;
        SetCubeValue(settings.CubeValue);
    }

    public void AddImpulse()
    {
        float forceX = Random.Range(-kMaxImpulseForce, kMaxImpulseForce);
        float forceY = Random.Range(kMinImpulseForce, kMaxImpulseForce);
        float forceZ = Random.Range(kMinImpulseForce, kMaxImpulseForce);
        _rb.AddForce(new Vector3(forceX, forceY, forceZ), ForceMode.Impulse);
    }

    public void ResetCube()
    {
        SetPosition(Vector3.zero);
        _transform.rotation = Quaternion.identity;
        _rb.linearVelocity = Vector3.zero;
    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public void LockCube()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.freezeRotation = true;
    }

    public void LaunchCube(float accelerationSpeed)
    {
        _impulseRoutine = StartCoroutine(StartImpulseRoutine(accelerationSpeed));
    }

    public void StopMovingRoutine()
    {
        if (_impulseRoutine != null)
            StopCoroutine(_impulseRoutine);
    }

    public bool IsMoving()
    {
        return Rigidbody.linearVelocity != Vector3.zero;
    }

    private IEnumerator StartImpulseRoutine(float accelerationSpeed)
    {
        _rb.freezeRotation = false;
        _rb.AddForce(new Vector3(0, 0, accelerationSpeed), ForceMode.Impulse);
        yield return new WaitForSeconds(kPostMovementDelay);

        OnCubeLauched();
    }

    private void SetCubeValue(int number)
    {
        foreach (var item in _cubeNumber)
        {
            item.text = number.ToString();
        }
    }

    private void OnCubeCollides(Cube current, Cube second)
    {
        CubesCollides?.Invoke(current, second);
    }

    private void OnCubeLauched()
    {
        CubeLauched?.Invoke();
    }
}
