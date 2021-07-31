using Zenject;

class BobbingWhileRun : BobbingChangeWhileAction
{
    [Inject] readonly MovementSpeed m_playerSpeed;

    protected override void Subscribe()
    {
        m_playerSpeed.OnPlayerRun += ChangeBobbingDuringRun;
        m_playerSpeed.OnPlayerStoppedRun += m_cameraBobbing.ResetBobbingValues;
    }

    protected override void Unsubscribe()
    {
        m_playerSpeed.OnPlayerRun -= ChangeBobbingDuringRun;
        m_playerSpeed.OnPlayerStoppedRun -= m_cameraBobbing.ResetBobbingValues;
    }
}