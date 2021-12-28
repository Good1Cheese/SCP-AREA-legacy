using Zenject;

public class BleedingEffectsController : EffectsController
{
    [Inject] protected readonly PlayerBlood _playerBlood;
    [Inject] protected readonly PlayerHealth _playerHealt;

    protected override void SubscribeToActions()
    {
        _playerBlood.Bled += SetEffectTimeAfterDamage;
        _playerBlood.Healed += SetEffectTimeAfterHeal;
        _playerHealt.Died += OnDestroy;
    }

    protected override float GetEffectTargetTime()
    {
        return _maxEffectTime * (_playerBlood.MaxAmount - _playerBlood.Amount) / 100;
    }

    protected override void UnsubscribeFromActions()
    {
        _playerBlood.Bled -= SetEffectTimeAfterDamage;
        _playerBlood.Healed -= SetEffectTimeAfterHeal;
        _playerHealt.Died -= OnDestroy;
    }
}