using Zenject;

public class MovementSaving : DataSaving
{
    [Inject] private readonly MovementController _movementController;
    [Inject] private readonly SlowWalkController _slowWalkController;

    public float speed;
    public float moveTime;
    public bool isSlowWalkUsing;

    public override void Save()
    {
        speed = _movementController.Speed;
        moveTime = _movementController.MoveTime;
        isSlowWalkUsing = _slowWalkController.IsMoving;
    }

    public override void Load()
    {
        _movementController.Speed = speed;
        _movementController.MoveTime = moveTime;

        if (!isSlowWalkUsing) { return; }

        _slowWalkController.IsMoving = true;
        _slowWalkController.UseStarted?.Invoke();
        _slowWalkController.GetSpeed();
    }
}
