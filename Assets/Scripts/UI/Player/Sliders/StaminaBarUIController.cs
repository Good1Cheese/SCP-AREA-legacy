using Zenject;

public class StaminaBarUIController : StatisticsBarUIController
{
    [Inject] private readonly PlayerStamina _playerStamina;

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