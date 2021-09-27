using Zenject;

public class PlayerRunSound : SoundPlayerOnAction
{
    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerMovement m_playerMovement;
    [Inject] readonly PlayerStamina m_playerStamina;
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
        m_playerSpeed.OnPlayerRun += PlaySound;
        m_playerSpeed.OnPlayerStoppedRun += StopSound;
        m_playerStamina.OnStaminaRanOut += StopSound;
        m_playerMovement.OnPlayerStoppedMoving += StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        m_playerSpeed.OnPlayerRun -= PlaySound;
        m_playerSpeed.OnPlayerStoppedRun -= StopSound;
        m_playerStamina.OnStaminaRanOut -= StopSound;
        m_playerMovement.OnPlayerStoppedMoving -= StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }
}

