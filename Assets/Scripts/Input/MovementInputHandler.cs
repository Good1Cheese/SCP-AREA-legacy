using System;
using UnityEngine;
using Zenject;

public class MovementInputHandler : MonoBehaviour
{
    [SerializeField] private float _inputSmoothTime;

    private InputContainer _inputContainer;
    private Vector2 _handledInput;
    private Vector2 _currentVelocity;

    public Action<Vector2> Handled { get; set; }

    [Inject]
    public void Construct(InputContainer inputContainer)
    {
        _inputContainer = inputContainer;
    }

    private void Update()
    {
        var currentInput = _inputContainer.Movement.ReadValue<Vector2>();
        _handledInput = Vector2.SmoothDamp(_handledInput, currentInput, ref _currentVelocity, _inputSmoothTime);

        Handled?.Invoke(_handledInput);
    }
}