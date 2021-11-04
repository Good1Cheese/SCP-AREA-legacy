using Zenject;

public class SlowWalkDetector : MoveDetector
{
    [Inject]
    void Construct(SlowWalkController slowWalkController)
    {
        m_moveController = slowWalkController;
    }
}
