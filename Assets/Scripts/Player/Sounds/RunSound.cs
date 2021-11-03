using Zenject;

public class RunSound : SoundPlayerOnAction
{
    [Inject] readonly RunController m_runController;
    [Inject] readonly PlayerStamina m_playerStamina;
    [Inject] readonly PlayerMovement m_playerMovement;
    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;

    protected override void PlaySound()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.Play();
    }
    protected override void StopSound()
    {
        audioSource.Stop();
    }

    protected override void SubscribeToAction()
    {
        m_runController.OnPlayerStartedUseOfMove += PlaySound;
        m_playerStamina.OnStaminaRanOut += StopSound;
        m_runController.OnPlayerStoppedUseOfMove += StopSound;
        m_playerMovement.OnPlayerStoppedMoving += StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        m_runController.OnPlayerStartedUseOfMove -= PlaySound;
        m_playerStamina.OnStaminaRanOut -= StopSound;
        m_runController.OnPlayerStoppedUseOfMove -= StopSound;
        m_playerMovement.OnPlayerStoppedMoving -= StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }
}

