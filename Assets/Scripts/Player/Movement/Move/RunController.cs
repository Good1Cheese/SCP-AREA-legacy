using Zenject;

public class RunController : MoveController
{
    private PlayerStamina _playerStamina;
    protected SlowWalkController _slowWalkController;
    protected SlowWalkRunController _slowWalkRunController;
    private MovementInputLink _playerMovement;

    [Inject]
    private void Construct(PlayerStamina playerStamina,
                           SlowWalkController slowWalkController,
                           SlowWalkRunController slowWalkRunController,
                           MovementInputLink playerMovement)
    {
        _playerStamina = playerStamina;
        _slowWalkController = slowWalkController;
        _slowWalkRunController = slowWalkRunController;
        _playerMovement = playerMovement;
    }

    public override float GetMove()
    {
        if (_slowWalkController.IsMoving) { return 0; }

        if (_slowWalkRunController.IsMoving)
        {
            UseStarted?.Invoke();
        }

        return Run();
    }

    protected float Run()
    {
        if (_playerMovement.VerticalMove < 0) { return 0; }

        if (_playerStamina.Amount <= 0)
        {
            return 0;
        }

        return base.GetMove();
    }
}