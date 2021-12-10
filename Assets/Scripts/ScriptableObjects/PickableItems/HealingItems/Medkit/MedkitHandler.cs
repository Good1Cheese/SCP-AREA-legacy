using Zenject;

public class MedkitHandler : InjectableItemHandler, IHealthInjectable
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject] private readonly CharacterBleeding _playerBleeding;

    private bool _isInjectUsed;

    public override void Inject()
    {
        _isInjectUsed = true;
        Use();
    }

    public override void Use()
    {
        _playerBleeding.StopAction();

        var medkit = (Medkit_SO)Item;
        _playerHealth.Heal(medkit.healthToHeal);
    }

    public override void Clicked(int slotIndex)
    {
        if (_isInjectUsed) { return; }

        base.Clicked(slotIndex);
        _numOfUses -= _numOfUses;
    }
}
