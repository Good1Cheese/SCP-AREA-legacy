using Zenject;

public class WalkSound : MoveSound
{
    [Inject]
    void Construct(WalkController walkController)
    {
        m_moveController = walkController;
    }
}
