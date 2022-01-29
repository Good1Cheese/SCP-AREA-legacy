using Zenject;

public class InjuryEffectsController : EffectsController
{
    protected PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    protected override void SubscribeToActions()
    {
        _playerHealth.Changed += InvokeCoroutine;
        _playerHealth.Died += OnDestroy;
    }

    protected override float GetEffectTargetTime()
    {
        return _maxEffectTime * _playerHealth.CurrentPercentage;
    }

    protected override void UnsubscribeFromActions()
    {
        _playerHealth.Changed += InvokeCoroutine;
        _playerHealth.Died -= OnDestroy;
    }
}