using UnityEngine;
using Zenject;

public class MovementInputGetter : MonoBehaviour
{
    [SerializeField] private float _minVelocityMagnitude;
    [SerializeField] private float _smoothTime;

    private InputContainer _inputContainer;
    private MovementInputLink _movementInputLink;

    private Vector2 _handledInput;
    private Vector2 _currentVelocity;

    [Inject]
    public void Construct(InputContainer inputContainer, MovementInputLink movementInputLink)
    {
        _inputContainer = inputContainer;
        _movementInputLink = movementInputLink;
    }

    private void Update()
    {
        var currentInput = _inputContainer.Movement.ReadValue<Vector2>();
        _handledInput = Vector2.SmoothDamp(_handledInput, currentInput, ref _currentVelocity, _smoothTime);

        if (_currentVelocity.magnitude <= _minVelocityMagnitude)
        {
            _handledInput = currentInput;
        }

        _movementInputLink.Handle(ref _handledInput);
    }
}