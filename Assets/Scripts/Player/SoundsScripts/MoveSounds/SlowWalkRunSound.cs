using Zenject;

public class SlowWalkRunSound : MoveSound
{
    [Inject]
    private void Construct(SlowWalkRun slowWalkRun)
    {
        _move = slowWalkRun;
    }
}