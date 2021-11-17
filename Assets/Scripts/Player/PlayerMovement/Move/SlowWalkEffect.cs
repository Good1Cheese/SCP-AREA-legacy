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
        _slowWalkController.OnPlayerStartedUseOfMove += SetSlowWalkTimeToZero;
        _slowWalkController.OnPlayerStoppedUseOfMove += SetSlowWalkTimeToMaxValue;
        _slowWalkController.OnPlayerUsingMove += ActivateEffect;
        _slowWalkController.OnPlayerNotUsingMove += DeactivateEffect;
    }

    private void SetSlowWalkTimeToZero()
    {
        SlowWalkTime = 0;
    }

    private void SetSlowWalkTimeToMaxValue()
    {
        SlowWalkTime = _yChangeTime;
    }

    private void ActivateEffect()
    {
        SlowWalkTime += Time.deltaTime;
        SetHeight();
    }

    private void DeactivateEffect()
    {
        SlowWalkTime -= Time.deltaTime;
        SetHeight();
    }

    public void SetHeight()
    {
        _characterController.height = _yForSlowWalk.Evaluate(SlowWalkTime);
    }

    private void OnDestroy()
    {
        _slowWalkController.OnPlayerStartedUseOfMove -= SetSlowWalkTimeToZero;
        _slowWalkController.OnPlayerStoppedUseOfMove -= SetSlowWalkTimeToMaxValue;
        _slowWalkController.OnPlayerUsingMove -= ActivateEffect;
        _slowWalkController.OnPlayerNotUsingMove -= DeactivateEffect;
    }
}
