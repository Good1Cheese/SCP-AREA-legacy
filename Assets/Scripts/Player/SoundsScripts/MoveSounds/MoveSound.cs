﻿using UnityEngine;
using Zenject;

public abstract class MoveSound : SoundOnAction
{
    [SerializeField] private AudioClip _clip;

    private PauseMenuToggler _pauseMenuToggler;
    private PlayerHealth _playerHealth;
    protected Move _move;

    [Inject]
    private void Construct(PauseMenuToggler pauseMenuToggler, PlayerHealth playerHealth)
    {
        _pauseMenuToggler = pauseMenuToggler;
        _playerHealth = playerHealth;
    }

    private void Start()
    {
        _playerHealth.Died += StopSoundOnPlayerDied;
    }

    private void StopSoundOnPlayerDied()
    {
        UnscribeToAction();

        if (!_audioSource.isPlaying) { return; }

        _audioSource.Stop();
    }

    protected override void PlaySound()
    {
        _audioSource.clip = _clip;

        if (_audioSource.isPlaying) { return; }

        _audioSource.Play();
    }

    protected override void SubscribeToAction()
    {
        _move.Actions.Stepped += PlaySound;
        _move.Actions.UseStopped += StopSound;
        _pauseMenuToggler.Toggled += StopSound;
    }

    protected override void UnscribeToAction()
    {
        _move.Actions.Stepped -= PlaySound;
        _move.Actions.UseStopped -= StopSound;
        _pauseMenuToggler.Toggled -= StopSound;
    }

    private new void OnDestroy()
    {
        base.OnDestroy();

        _playerHealth.Died -= StopSoundOnPlayerDied;
    }
}