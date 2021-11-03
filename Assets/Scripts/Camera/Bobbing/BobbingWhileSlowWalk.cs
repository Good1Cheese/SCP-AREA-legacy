using Zenject;

class BobbingWhileSlowWalk : BobbingChangeWhileMoveAction
{
    [Inject]
    void Construct(SlowWalkController slowWalkController)
    {
        m_moveController = slowWalkController;
    }
}