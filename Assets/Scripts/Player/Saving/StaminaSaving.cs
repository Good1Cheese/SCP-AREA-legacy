using Zenject;

public class StaminaSaving : DataSaving
{
    [Inject] private readonly PlayerStamina _playerStamina;

    public float staminaTime;
    public bool hasTimeoutPassed;

    public override void Save()
    {
        staminaTime = _playerStamina.CurveTime;
        hasTimeoutPassed = _playerStamina.IsTimeoutPassed;
    }

    public override void LoadData()
    {
        _playerStamina.CurveTime = staminaTime;
        
        if (hasTimeoutPassed)
        {
            _playerStamina.StartActionWithInterrupt();
        }
    }
}