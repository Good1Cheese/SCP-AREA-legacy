using UnityEngine;
using Zenject;

public abstract class MoveSound : SoundOnAction
{
    [SerializeField] private AudioClip _clip;

    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenu;

    protected MoveController _moveController;

    protected override void PlaySound()
    {
        _audioSource.clip = _clip;
        if (_audioSource.isPlaying) { return; }
        _audioSource.Play();
    }

    protected override void SubscribeToAction()
    {
        _moveController.OnPlayerUsingMove += PlaySound;
        _moveController.OnPlayerStoppedUseOfMove += StopSound;
        _pauseMenu.OnPauseMenuButtonPressed += StopSound;
    }

    protected override void UnscribeToAction()
    {
        _moveController.OnPlayerUsingMove -= PlaySound;
        _moveController.OnPlayerStoppedUseOfMove -= StopSound;
        _pauseMenu.OnPauseMenuButtonPressed -= StopSound;
    }
}
