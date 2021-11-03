using Zenject;

public class WalkSound : SoundPlayerOnAction
{
    [Inject] readonly RunController m_runController;
    [Inject] readonly MovementController m_movementController;
    [Inject] readonly PlayerMovement m_playerMovement;
    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;

    protected override void PlaySound()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.Play();
    }

    protected override void SubscribeToAction()
    {
        m_movementController.OnPlayerWalking += PlaySound;
        m_runController.OnPlayerStartedUseOfMove += StopSound;
        m_playerMovement.OnPlayerStoppedMoving += StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        m_movementController.OnPlayerWalking -= PlaySound;
        m_runController.OnPlayerStartedUseOfMove -= StopSound;
        m_playerMovement.OnPlayerStoppedMoving -= StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }
}
