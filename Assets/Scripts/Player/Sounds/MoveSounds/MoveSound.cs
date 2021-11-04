using UnityEngine;
using Zenject;

public abstract class MoveSound : SoundOnAction
{
    [SerializeField] AudioClip m_clip;

    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;

    protected MoveController m_moveController;

    protected override void PlaySound()
    {
        m_audioSource.clip = m_clip;
        if (m_audioSource.isPlaying) { return; }
        m_audioSource.Play();
    }

    protected override void SubscribeToAction()
    {
        m_moveController.OnPlayerUsingMove += PlaySound;
        m_moveController.OnPlayerStoppedUseOfMove += StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        m_moveController.OnPlayerUsingMove -= PlaySound;
        m_moveController.OnPlayerStoppedUseOfMove -= StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }
}
