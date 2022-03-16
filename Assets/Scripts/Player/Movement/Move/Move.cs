using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public abstract class Move : MonoBehaviour
{
    [SerializeField] private float _maxMoveTime;
    [SerializeField] private float _valueForFov;
    [SerializeField] private float _targetStepTime;

    protected DynamicFov _dynamicFov;
    protected MovesContainer _movesContainer;
    protected InputContainer _inputContainer;

    public float MaxMoveTime => _maxMoveTime;
    public float TargetStepTime => _targetStepTime;
    public MoveActions Actions { get; } = new MoveActions();
    public bool Using { get; set; }

    [Inject]
    private void Construct(DynamicFov dynamicFov, MovesContainer movesContainer, InputContainer inputContainer)
    {
        _dynamicFov = dynamicFov;
        _movesContainer = movesContainer;
        _inputContainer = inputContainer;
    }

    protected void Perform(InputAction.CallbackContext _)
    {
        Actions.UseStarted?.Invoke();
        Using = true;
    }

    protected void Cancel(InputAction.CallbackContext _)
    {
        Actions.UseStopped?.Invoke();
        Using = false;
    }

    public virtual void Use()
    {
        if (Using)
        {
            UpdateMoveTime();
            Actions.Using?.Invoke();

            return;
        }

        Actions.NotUsing?.Invoke();
    }

    protected void UpdateMoveTime()
    {
        if (_movesContainer.MoveTime < MaxMoveTime)
        {
            _movesContainer.MoveTime += Time.deltaTime;
            return;
        }

        _movesContainer.MoveTime -= Time.deltaTime;
    }

    public void UpdateFov() => _dynamicFov.SetFov(_valueForFov);

    private void Start() => Subscribe();
    private void OnDestroy() => Unsubscribe();

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}