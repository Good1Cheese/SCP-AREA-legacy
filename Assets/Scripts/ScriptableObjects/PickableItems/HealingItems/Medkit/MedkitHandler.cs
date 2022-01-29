using Zenject;

public class MedkitHandler : StackableItemHandler, IHealthInjectable
{
    private PlayerHealth _playerHealth;
    private PlayerBlood _playerBlood;

    private bool _isInjectUsed;

    [Inject]
    private void Inject(PlayerHealth playerHealth, PlayerBlood playerBlood)
    {
        _playerHealth = playerHealth;
        _playerBlood = playerBlood;
    }

    public void Inject()
    {
        _isInjectUsed = true;
        Use();
    }

    public override void Use()
    {
        _playerBlood.StopCoroutine();

        var medkit = (Medkit_SO)Item_SO;
        _playerHealth.Heal(medkit.healthToHeal);
    }

    public override void Clicked(int slotIndex)
    {
        if (_isInjectUsed) { return; }

        base.Clicked(slotIndex);
    }
}