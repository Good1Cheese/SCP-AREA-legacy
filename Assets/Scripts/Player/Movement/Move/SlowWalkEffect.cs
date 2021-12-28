using System;
using UnityEngine;
using Zenject;

public class SlowWalkEffect : MonoBehaviour
{
    private const int START_CHARACTER_CONTROLLER_HEIGHT = 2;

    [SerializeField] private float _yChangeTime;
    [SerializeField] private AnimationCurve _yForSlowWalk;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _headParent;

    [Inject] private readonly SlowWalkController _slowWalkController;
    private Vector3 _down = Vector3.down;

    public float SlowWalkTime { get; set; }

    private void Start()
    {
        _slowWalkController.Using += ActivateEffect;
        _slowWalkController.NotUsing += DeactivateEffect;
    }

    private void ActivateEffect()
    {
        SetHeight(() => SlowWalkTime >= _yChangeTime, 1);
    }

    private void DeactivateEffect()
    {
        SetHeight(() => SlowWalkTime <= 0, -1);
    }

    public void SetHeight(Func<bool> condition, sbyte deltaTimeMultypliyer)
    {
        if (condition.Invoke()) { return; }

        SlowWalkTime += Time.deltaTime * deltaTimeMultypliyer;
        _characterController.height = _yForSlowWalk.Evaluate(SlowWalkTime);
        SetOffset();
    }

    private void SetOffset()
    {
        Vector3 offset = _down * (START_CHARACTER_CONTROLLER_HEIGHT - _characterController.height) / 2;

        _characterController.center = offset;
        _headParent.localPosition = offset;
    }

    private void OnDestroy()
    {
        _slowWalkController.Using -= ActivateEffect;
        _slowWalkController.NotUsing -= DeactivateEffect;
    }
}