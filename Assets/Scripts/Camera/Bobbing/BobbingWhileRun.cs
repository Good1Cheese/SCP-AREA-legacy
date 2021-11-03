using Zenject;

class BobbingWhileRun : BobbingChangeWhileMoveAction
{
    [Inject]
    void Construct(RunController runController)
    {
        m_moveController = runController;
    }
}
