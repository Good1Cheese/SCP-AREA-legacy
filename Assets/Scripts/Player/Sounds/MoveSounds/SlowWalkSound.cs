using Zenject;

public class SlowWalkSound : MoveSound
{
    [Inject]
    void Construct(SlowWalkController slowWalkController)
    {
        m_moveController = slowWalkController;
    }
}