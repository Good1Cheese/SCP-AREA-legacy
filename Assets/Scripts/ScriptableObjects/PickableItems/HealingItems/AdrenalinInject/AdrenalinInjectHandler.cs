using Zenject;

public class AdrenalinInjectHandler : InjectableItemHandler, IAdrenalinInjectable
{
    [Inject] private readonly StaminaUseDisabler _staminaUseDisabler;
    [Inject] private readonly PlayerHealth _playerHealth;

    public AdrenalinInject_SO AdrenalinInject_SO => _pickableIte_SO as AdrenalinInject_SO;

    public new void Inject()
    {
        _staminaUseDisabler.Disable(AdrenalinInject_SO.adrenalineTime);
        _playerHealth.AddAdrenalineHealth(AdrenalinInject_SO.adrenalineHealthAmount);
    }

    public override bool ShouldItemNotBeUsed => false;
}