using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SlowWalkEffect))]
public class SlowWalkController : MoveController
{
    [Inject] private readonly PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement.NotMoving += Da;
    }

    private void Da()
    {
        if (Input.GetKey(_key))
        {
            Using?.Invoke();
            return;
        }

        NotUsing?.Invoke();
    }


    private void OnDestroy()
    {
        _playerMovement.NotMoving -= Da;
    }
}
