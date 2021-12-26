using Zenject;

public class RunController : MoveController
{
    [Inject] private readonly PlayerStamina _playerStamina;
    [Inject] protected readonly SlowWalkController _slowWalkController;
    [Inject] protected readonly SlowWalkRunController _slowWalkRunController;
    [Inject] private readonly PlayerMovement _playerMovement;

    public override float GetMove()
    {
        if (_slowWalkController.IsMoving) { return 0; }

        if (_slowWalkRunController.IsMoving)
        {
            UseStarted?.Invoke();
        }

        return Run();
    }
    public override void CalculateFov() => _dynamicFov.SetFov(0.5f);

    protected float Run()
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