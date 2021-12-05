using Zenject;

public class AdrenalinInjectHandler : InjectableItemHandler, IAdrenalinInjectable
{
    [Inject] private readonly StaminaUseDisabler _staminaUseDisabler;

    public AdrenalinInject_SO AdrenalinInject_SO => _pickableIte_SO as AdrenalinInject_SO;

    public new void Inject()
    {
        _staminaUseDisabler.Disable(AdrenalinInject_SO.adrenalineTime);
    }

    public override bool ShouldItemNotBeUsed => false;
}