using Zenject;

public class RunController : MoveController
{
    [Inject] private readonly PlayerStamina _playerStamina;
    [Inject] private readonly PlayerMovement _playerMovement;

    public override float GetMove()
    {
        if (_playerMovement.VerticalMove < 0) { return 0; }

        if (_playerStamina.Stamina <= 0)
        {
            _playerStamina.RanOut?.Invoke();
            return 0;
        }

        return base.GetMove();
    }
}