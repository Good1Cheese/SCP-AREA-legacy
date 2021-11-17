using Zenject;

public class MovementSaving : DataSaving
{
    [Inject] private readonly MovementController _movementController;

    public float speed;
    public float moveTime;

    public override void Save()
    {
        speed = _movementController.Speed;
        moveTime = _movementController.MoveTime;
    }

    public override void LoadData()
    {
        _movementController.Speed = speed;
        _movementController.MoveTime = moveTime;
    }
}
