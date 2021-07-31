using Zenject;

public class PlayerRunSound : SoundPlayerOnAction
{
    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerMovement m_playerMovement;

    protected override void PlaySound()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.Play();
    }

    protected override void SubscribeToAction()
    {
        m_playerSpeed.OnPlayerRun += PlaySound;
        m_playerSpeed.OnPlayerStoppedRun += StopSound;
        m_playerMovement.OnPlayerStoppedMoving += StopSound;
    }

    protected override void UnscribeToAction()
    {
        m_playerSpeed.OnPlayerRun -= PlaySound;
        m_playerSpeed.OnPlayerStoppedRun -= StopSound;
        m_playerMovement.OnPlayerStoppedMoving += StopSound;
    }
}

