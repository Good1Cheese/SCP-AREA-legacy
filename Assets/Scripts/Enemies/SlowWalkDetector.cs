using Zenject;

public class SlowWalkDetector : MoveDetector
{
    [Inject]
    private void Construct(SlowWalk slowWalk)
    {
        _move = slowWalk;
    }
}
