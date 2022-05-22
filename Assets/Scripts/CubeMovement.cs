using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float _accelerationSpeed;
    [SerializeField] private float _speed;

    private Rigidbody _rb;
    private float _directionX = 0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        PlayerInput.OnMove += Move;
    }

    private void OnDisable()
    {
        PlayerInput.OnMove -= Move;
    }

    private void FixedUpdate()
    {
        float posX = _rb.position.x + _directionX * _speed * Time.deltaTime;
        
        _rb.MovePosition(new Vector3(posX, _rb.position.y,_rb.position.z));
    }

    private void Move(float directionX)
    {
        _directionX = directionX;
    }
    
    
    public void AddImpulse() => _rb.AddForce(new Vector3(0,0,_accelerationSpeed), ForceMode.Impulse);
}
