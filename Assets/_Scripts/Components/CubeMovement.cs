using Game.Managers;
using Game.Settings;
using System;
using UnityEngine;
using Zenject;

public class CubeMovement : IFixedTickable, IDisposable
{
    public event Action CubeMovementEnded;

    private readonly IInputManager _inputManager;
    private readonly CubeAccelerationSettings _settings;

    private bool _canMove = true;
    private Rigidbody _rb;
    private float _directionX = 0f;
    private Cube _currentCube;

    public CubeMovement(IInputManager inputManager, CubeAccelerationSettings settings)
    {
        _settings = settings;
        _inputManager = inputManager;
        _inputManager.HorizontalInputChanged += SetCubeDirection;
        _inputManager.ShootTriggered += ShootCube;
    }

    public void SetCurrentCube(Cube cube)
    {
        UnsubscribeFromCubeEvents();

        _currentCube = cube;
        _currentCube.CubeLauched += OnCubeLaunched;
        _currentCube.LockCube();
        _rb = cube.Rigidbody;
    }
    public void FixedTick()
    {
        float posX = _rb.position.x + _directionX * _settings.Speed * Time.deltaTime;
        _rb.MovePosition(new Vector3(posX, _rb.position.y, _rb.position.z));
    }

    public void Dispose()
    {
        UnsubscribeFromCubeEvents();
        _inputManager.HorizontalInputChanged -= SetCubeDirection;
        _inputManager.ShootTriggered -= ShootCube;
    }

    private void SetCubeDirection(float directionX)
    {
        if(_canMove) 
            _directionX = directionX;
    }

    private void ShootCube()
    {
        _canMove = false;
        _currentCube.StopMovingRoutine();
        _currentCube.LaunchCube(_settings.AccelerationSpeed);
    }

    private void UnsubscribeFromCubeEvents()
    {
        if (_currentCube == null)
            return;

        _currentCube.CubeLauched -= OnCubeLaunched;
    }

    private void OnCubeLaunched()
    {
        _canMove = true;
        OnCubeMovementEnded();
    }

    private void OnCubeMovementEnded()
    {
        CubeMovementEnded?.Invoke();
    }
}
