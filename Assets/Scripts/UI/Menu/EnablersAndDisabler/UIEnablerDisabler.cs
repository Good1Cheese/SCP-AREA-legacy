using System;
using Zenject;

public abstract class UIEnablerDisabler : InteractableWithDelay
{
    private PlayerHealth _playerHealth;
    private GameLoader _gameLoader;

    public bool IsActivated { get; set; }
    public Action EnabledDisabled { get; set; }

    [Inject]
    private void Construct(PlayerHealth playerHealth, GameLoader gameLoader)
    {
        _playerHealth = playerHealth;
        _gameLoader = gameLoader;
    }

    private new void Awake()
    {
        base.Awake();
        _playerHealth.Died += DisableUI;
        _gameLoader.UILoading += SetScriptActiveState;
    }

    private void DisableUI()
    {
        if (IsActivated)
        {
            Interact();
        }

        enabled = false;
    }

    private void SetScriptActiveState(bool activeState)
    {
        enabled = activeState;
    }

    private void OnDestroy()
    {
        _playerHealth.Died -= DisableUI;
        _gameLoader.UILoading -= SetScriptActiveState;
    }
}