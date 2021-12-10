using Zenject;

public class AdrenalinInjectHandler : InjectableItemHandler, IAdrenalinInjectable
{
    [Inject] private readonly StaminaDisabler _staminaUseDisabler;

    public AdrenalinInject_SO AdrenalinInject_SO => _pickableIte_SO as AdrenalinInject_SO;

    public override void Inject()
    {
        _staminaUseDisabler.Disable(AdrenalinInject_SO.adrenalineEffectTime);
    }

    public override bool ShouldItemNotBeUsed => false;
}