using Zenject;

public class SlowWalkSound : MoveSound
{
    [Inject]
    private void Construct(SlowWalk slowWalk)
    {
        _move = slowWalk;
    }
}