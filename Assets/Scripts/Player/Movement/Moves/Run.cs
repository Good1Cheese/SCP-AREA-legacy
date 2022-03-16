using Zenject;

public class Run : Move
{
    protected PlayerStamina _playerStamina;
    protected MovementInputLink _movementInputLink;
    protected SlowWalk _slowWalk;
    protected SlowWalkRun _slowWalkRun;

    [Inject]
    private void Construct(PlayerStamina playerStamina,
                           SlowWalk slowWalk,
                           SlowWalkRun slowWalkRun,
                           MovementInputLink playerMovement)
    {
        _playerStamina = playerStamina;
        _slowWalk = slowWalk;
        _slowWalkRun = slowWalkRun;
        _movementInputLink = playerMovement;
    }

    public override void Use()
    {
        if (_slowWalk.Using) { return; }

        UseRun();
    }

    protected void UseRun()
    {
        if (_movementInputLink.VerticalMove < 0 || _playerStamina.Amount <= 0) { return; }

        base.Use();
    }

    protected override void Subscribe()
    {
        _inputContainer.Run.performed += Perform;
        _inputContainer.Run.canceled += Cancel;
    }

    protected override void Unsubscribe()
    {
        _inputContainer.Run.performed -= Perform;
        _inputContainer.Run.canceled -= Cancel;
    }
}