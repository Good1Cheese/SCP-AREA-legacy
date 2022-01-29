using System;
using UnityEngine;
using Zenject;

public class SlowWalkEffect : MonoBehaviour
{
    [SerializeField] private float _yChangeTime;
    [SerializeField] private AnimationCurve _yForSlowWalk;
    [SerializeField] private Transform _headParent;

    [Inject] private readonly CharacterController _characterController;
    [Inject] private readonly SlowWalkController _slowWalkController;
    private Vector3 _down = Vector3.down;

    private float _startCharacterControllerHeight;

    public float SlowWalkTime { get; set; }

    private void Start()
    {
        _startCharacterControllerHeight = _characterController.height;
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
        Vector3 offset = _down * (_startCharacterControllerHeight - _characterController.height) / 2;

        _characterController.center = offset;
        _headParent.localPosition = offset;
    }

    private void OnDestroy()
    {
        _slowWalkController.Using -= ActivateEffect;
        _slowWalkController.NotUsing -= DeactivateEffect;
    }
}