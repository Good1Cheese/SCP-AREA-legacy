using Zenject;

public class RunSound : MoveSound
{
    [Inject] readonly PlayerStamina m_playerStamina;

    [Inject]
    void Construct(RunController runController)
    {
        m_moveController = runController;
    }

    protected override void SubscribeToAction()
    {
        base.SubscribeToAction();
        m_playerStamina.OnStaminaRanOut += StopSound;
    }

    protected override void UnscribeToAction()
    {
        base.UnscribeToAction();
        m_playerStamina.OnStaminaRanOut -= StopSound;
    }
}

