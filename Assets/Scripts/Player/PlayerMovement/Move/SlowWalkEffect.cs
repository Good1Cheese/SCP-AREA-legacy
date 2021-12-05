using System;
using UnityEngine;
using Zenject;

public class SlowWalkEffect : MonoBehaviour
{
    [SerializeField] private float _yChangeTime;
    [SerializeField] private AnimationCurve _yForSlowWalk;
    [SerializeField] private CharacterController _characterController;

    [Inject] private readonly SlowWalkController _slowWalkController;

    public float SlowWalkTime { get; set; }

    private void Start()
    {
        _slowWalkController.OnPlayerUsingMove += ActivateEffect;
        _slowWalkController.OnPlayerNotUsingMove += DeactivateEffect;
    }

    private void ActivateEffect()
    {
        SetHeight(() =>
        {
            return SlowWalkTime >= _yChangeTime;
        }, 1);
    }

    private void DeactivateEffect()
    {
        SetHeight(() =>
        {
            return SlowWalkTime <= 0;
        }, -1);
    }

    public void SetHeight(Func<bool> condition, sbyte deltaTimeMultypliyer)
    {
        if (condition.Invoke()) { return; }

        SlowWalkTime += Time.deltaTime * deltaTimeMultypliyer;
        _characterController.height = _yForSlowWalk.Evaluate(SlowWalkTime);
    }

    private void OnDestroy()
    {
        _slowWalkController.OnPlayerUsingMove -= ActivateEffect;
        _slowWalkController.OnPlayerNotUsingMove -= DeactivateEffect;
    }
}
