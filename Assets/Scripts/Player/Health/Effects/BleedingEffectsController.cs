using Zenject;

public class BleedingEffectsController : EffectsController
{
    protected PlayerBlood _playerBlood;
    protected PlayerHealth _playerHealth;

    [Inject]
    private void Construct(PlayerHealth playerHealth, PlayerBlood playerBlood)
    {
        _playerHealth = playerHealth;
        _playerBlood = playerBlood;
    }

    protected override void SubscribeToActions()
    {
        _playerBlood.Changed += InvokeCoroutine;
        _playerHealth.Died += OnDestroy;
    }

    protected override float GetEffectTargetTime()
    {
        return _maxEffectTime * _playerBlood.CurrentPercentage;
    }

    protected override void UnsubscribeFromActions()
    {
        _playerBlood.Changed -= InvokeCoroutine;
        _playerHealth.Died -= OnDestroy;
    }
}