using Zenject;

class BobbingWhileSneak : BobbingChangeWhileAction
{
    [Inject] PlayerMovementSpeed m_playerSpeed;

    protected override void Subscribe()
    {
        m_playerSpeed.OnPlayerSneak += ChangeBobbingDuringRun;
        m_playerSpeed.OnPlayerStoppedSneak += m_cameraBobbing.ResetBobbingValues;
    }

    protected override void Unsubscribe()
    {
        m_playerSpeed.OnPlayerSneak -= ChangeBobbingDuringRun;
        m_playerSpeed.OnPlayerStoppedSneak -= m_cameraBobbing.ResetBobbingValues;
    }
}

