using Zenject;

public class AdrenalinInjectHandler : StackableItemHandler, IAdrenalinInjectable
{
    private StaminaDisabler _staminaDisabler;

    public AdrenalinInject_SO AdrenalinInject_SO => _pickableItem_SO as AdrenalinInject_SO;

    [Inject]
    private void Inject(StaminaDisabler staminaDisabler)
    {
        _staminaDisabler = staminaDisabler;
    }

    public void Inject()
    {
        _staminaDisabler.Disable(AdrenalinInject_SO.adrenalineEffectTime);
    }

    public override bool ShouldItemNotBeUsed => false;
}