using Zenject;

public class MovementSaving : DataSaving
{
    [Inject] private readonly MoveSpeed _moveSpeed;
    [Inject] private readonly SlowWalkController _slowWalkController;

    public float speed;
    public float moveTime;
    public bool isSlowWalkUsing;

    public override void Save()
    {
        speed = _moveSpeed.Speed;
        moveTime = _moveSpeed.MoveTime;
        isSlowWalkUsing = _slowWalkController.IsMoving;
    }

    public override void Load()
    {
        _moveSpeed.Speed = speed;
        _moveSpeed.MoveTime = moveTime;

        if (!isSlowWalkUsing) { return; }

        _slowWalkController.IsMoving = true;
        _slowWalkController.UseStarted?.Invoke();
        _slowWalkController.GetSpeed();
    }
}
