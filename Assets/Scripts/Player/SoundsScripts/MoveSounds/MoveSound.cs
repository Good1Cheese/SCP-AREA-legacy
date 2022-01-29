using UnityEngine;
using Zenject;

public abstract class MoveSound : SoundOnAction
{
    [SerializeField] private AudioClip _clip;

    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private PlayerHealth _playerHealth;
    protected MoveController _moveController;

    [Inject]
    private void Construct(PauseMenuEnablerDisabler pauseMenuEnablerDisabler, PlayerHealth playerHealth)
    {
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
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
        _moveController.Stepped += PlaySound;
        _moveController.UseStopped += StopSound;
        _pauseMenuEnablerDisabler.EnabledDisabled += StopSound;
    }

    protected override void UnscribeToAction()
    {
        _moveController.Stepped -= PlaySound;
        _moveController.UseStopped -= StopSound;
        _pauseMenuEnablerDisabler.EnabledDisabled -= StopSound;
    }

    private new void OnDestroy()
    {
        base.OnDestroy();

        _playerHealth.Died -= StopSoundOnPlayerDied;
    }
}