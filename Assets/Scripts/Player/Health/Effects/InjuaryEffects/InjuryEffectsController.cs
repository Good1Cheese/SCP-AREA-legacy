using Zenject;

public class InjuryEffectsController : EffectsController
{
    [Inject] protected readonly PlayerHealth _playerHealth;

    protected override void SubscribeToActions()
    {
        _playerHealth.Changed += UpdateCoroutine;
        _playerHealth.Died += OnDestroy;
    }

    protected override float GetEffectTargetTime()
    {
        return _maxEffectTime * _playerHealth.CurrentPercentage;
    }

    protected override void UnsubscribeFromActions()
    {
        _playerHealth.Changed += UpdateCoroutine;
        _playerHealth.Died -= OnDestroy;
    }
}