using Zenject;

public class StaminaSaving : DataSaving
{
    [Inject] private readonly PlayerStamina _playerStamina;

    public float staminaTime;
    public bool hasTimeoutPassed;

    public override void Save()
    {
        staminaTime = _playerStamina.StaminaTime;
        hasTimeoutPassed = _playerStamina.HasTimeoutPassed;
    }

    public override void LoadData()
    {
        _playerStamina.StaminaTime = staminaTime;
        _playerStamina.HasTimeoutPassed = hasTimeoutPassed;
    }
}
