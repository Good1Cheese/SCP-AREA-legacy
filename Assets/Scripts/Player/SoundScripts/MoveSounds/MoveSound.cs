using UnityEngine;
using Zenject;

public abstract class MoveSound : SoundOnAction
{
    [SerializeField] private AudioClip _clip;

    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenu;
    [Inject] readonly private PlayerHealth _playerHealth;

    protected MoveController _moveController;

    private void Start()
    {
        _playerHealth.OnPlayerDies += StopSoundOnPlayerDies;
    }

    private void StopSoundOnPlayerDies()
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
        _moveController.OnPlayerUsingMove += PlaySound;
        _moveController.OnPlayerStoppedUsing += StopSound;
        _pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        _moveController.OnPlayerUsingMove -= PlaySound;
        _moveController.OnPlayerStoppedUsing -= StopSound;
        _pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }

    private new void OnDestroy()
    {
        base.OnDestroy();

        _playerHealth.OnPlayerDies -= StopSoundOnPlayerDies;
    }
}
