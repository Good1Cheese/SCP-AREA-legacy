using Zenject;

public class StaminaBarUIController : StatisticsBarUIController
{
    private PlayerStamina _playerStamina;

    [Inject]
    private void Construct(PlayerStamina playerStamina)
    {
        _playerStamina = playerStamina;
    }

    protected override float GetValue()
    {
        return _playerStamina.Amount;
    }

    protected override void Subscribe()
    {
        _playerStamina.Changed += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerStamina.Changed -= UpdateUI;
    }
}