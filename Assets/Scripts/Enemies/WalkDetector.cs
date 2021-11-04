using Zenject;

public class WalkDetector : MoveDetector
{
    [Inject]
    void Construct(WalkController walkController)
    {
        m_moveController = walkController;
    }
}
