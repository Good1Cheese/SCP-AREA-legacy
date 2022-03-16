using Zenject;

public class RunSound : MoveSound
{
    [Inject]
    private void Construct(Run runController)
    {
        _move = runController;
    }
}