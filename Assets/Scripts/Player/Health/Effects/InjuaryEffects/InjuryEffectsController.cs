using Zenject;

public class InjuryEffectsController : EffectsController
{
    [Inject] protected readonly PlayerHealth _playerHealth;

    protected override void SubscribeToActions()
    {
        _playerHealth.Damaged += SetEffectTimeAfterDamage;
        _playerHealth.Healed += SetEffectTimeAfterHeal;
        _playerHealth.Died += OnDestroy;
    }

    protected override float GetEffectTargetTime()
    {
        return _maxEffectTime * (_playerHealth.MaxAmount - _playerHealth.Amount) / 100;
    }

    protected override void UnsubscribeFromActions()
    {
        _playerHealth.Damaged -= SetEffectTimeAfterDamage;
        _playerHealth.Healed -= SetEffectTimeAfterHeal;
        _playerHealth.Died -= OnDestroy;
    }
}