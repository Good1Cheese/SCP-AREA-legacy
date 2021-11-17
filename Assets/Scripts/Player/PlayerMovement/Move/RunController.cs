using Zenject;

public class RunController : MoveController
{
    [Inject] private readonly PlayerStamina _playerStamina;

    public override float GetMove()
    {
        if (_playerStamina.Stamina <= 0)
        {
            _playerStamina.OnStaminaRunningOut?.Invoke();
            return 0;
        }

        return base.GetMove();
    }
}
