using Zenject;

public class BleedingEffectsController : EffectsController
{
    [Inject] protected readonly PlayerBlood _playerBlood;
    [Inject] protected readonly PlayerHealth _playerHealth;

    protected override void SubscribeToActions()
    {
        _playerBlood.Changed += UpdateCoroutine;
        _playerHealth.Died += OnDestroy;
    }

    protected override float GetEffectTargetTime()
    {
        return _maxEffectTime * _playerBlood.CurrentPercentage;
    }

    protected override void UnsubscribeFromActions()
    {
        _playerBlood.Changed -= UpdateCoroutine;
        _playerHealth.Died -= OnDestroy;
    }
}