using UnityEngine;
using Zenject;

[RequireComponent(typeof(SlowWalkEffect))]
public class SlowWalk : Move
{
    protected override void Subscribe()
    {
        _inputContainer.SlowWalk.performed += Perform;
        _inputContainer.SlowWalk.canceled += Cancel;
    }

    protected override void Unsubscribe()
    {
        _inputContainer.SlowWalk.performed -= Perform;
        _inputContainer.SlowWalk.canceled -= Cancel;
    }
}