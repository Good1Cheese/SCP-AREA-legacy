using Zenject;

public class RunSound : MoveSound
{
    [Inject]
    private void Construct(RunController runController)
    {
        _moveController = runController;
    }
}

