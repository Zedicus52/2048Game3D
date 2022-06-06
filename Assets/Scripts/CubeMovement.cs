using System;
using System.Collections;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public event Action SpawnCube;
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _speed;

    private bool _canMove = true;
    private Rigidbody _rb;
    private float _directionX = 0f;
    

    private void OnEnable()
    {
        PlayerInput.OnMove += Move;
        PlayerInput.EndMove += AddImpulse;
    }

    private void OnDisable()
    {
        PlayerInput.OnMove -= Move;
        PlayerInput.EndMove -= AddImpulse;
    }

    private void FixedUpdate()
    {
        float posX = _rb.position.x + _directionX * _speed * Time.deltaTime;
        _rb.MovePosition(new Vector3(posX, _rb.position.y, _rb.position.z));
    }

    private void Move(float directionX)
    {
        if(_canMove) 
            _directionX = directionX;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            AddImpulse();
    }
#endif

    private void AddImpulse()
    {
        StartCoroutine(Impulse());
    }

    private IEnumerator Impulse()
    {
        _canMove = false;
        _rb.AddForce(new Vector3(0,0,_accelerationSpeed), ForceMode.Impulse);
        yield return new WaitForSecondsRealtime(0.5f);
        _rb.freezeRotation = false;
        SpawnCube?.Invoke();
        _directionX = 0f;
        _canMove = true;
    }

    public void SetCubeRigidbody(Rigidbody rb)
    {
        _rb = rb;
        _rb.velocity = Vector3.zero;
    }
}
