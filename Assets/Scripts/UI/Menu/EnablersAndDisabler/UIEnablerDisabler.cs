using System;
using UnityEngine;
using Zenject;

public abstract class UIEnablerDisabler : MonoBehaviour
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

    private void Awake()
    {
        _playerHealth.Died += DisableUI;
        _gameLoader.UILoading += SetScriptActiveState;
    }

    private void DisableUI()
    {
        if (IsActivated)
        {
            EnableDisableUI();
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

    public abstract void EnableDisableUI();
}