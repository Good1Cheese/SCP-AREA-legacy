﻿using Zenject;

public class PlayerWalkSound : SoundPlayerOnAction
{
    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerMovement m_playerMovement;
    [Inject] readonly PauseMenu m_pauseMenu;

    protected override void PlaySound()
    {
        if (audioSource.isPlaying) { return; }
        audioSource.Play();
    }

    protected override void SubscribeToAction()
    {
        m_playerSpeed.OnPlayerWalks += PlaySound;
        m_playerSpeed.OnPlayerRun += StopSound;
        m_playerMovement.OnPlayerStoppedMoving += StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        m_playerSpeed.OnPlayerWalks -= PlaySound;
        m_playerSpeed.OnPlayerRun -= StopSound;
        m_playerMovement.OnPlayerStoppedMoving -= StopSound;
        m_pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }
}
