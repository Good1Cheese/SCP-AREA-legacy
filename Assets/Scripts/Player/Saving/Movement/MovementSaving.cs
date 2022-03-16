using Zenject;

public class MovementSaving : DataSaving
{
    [Inject] private readonly MovesContainer _movesContainer;
    [Inject] private readonly SlowWalk _slowWalk;

    public float speed;
    public float moveTime;
    public bool isSlowWalkUsing;

    public override void Save()
    {
        speed = _movesContainer.Speed;
        moveTime = _movesContainer.MoveTime;
        isSlowWalkUsing = _slowWalk.Using;
    }

    public override void Load()
    {
        _movesContainer.Speed = speed;
        _movesContainer.MoveTime = moveTime;

        if (!isSlowWalkUsing) { return; }

        _slowWalk.Using = true;
        _slowWalk.Actions.UseStarted?.Invoke();
        _slowWalk.Use();
    }
}
