using Zenject;

public class SlowWalkSound : SoundPlayerOnAction
{
    [Inject] readonly SlowWalkController m_slowWalkController;
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
        m_slowWalkController.OnPlayerStartedUseOfMove += PlaySound;
        m_slowWalkController.OnPlayerStoppedUseOfMove += StopSound;
        m_playerMovement.OnPlayerStoppedMoving += StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        m_slowWalkController.OnPlayerStartedUseOfMove -= PlaySound;
        m_slowWalkController.OnPlayerStoppedUseOfMove -= StopSound;
        m_playerMovement.OnPlayerStoppedMoving -= StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }
}