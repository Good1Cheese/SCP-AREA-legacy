using UnityEngine;
using Zenject;

public abstract class UIEnablerDisabler : MonoBehaviour
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject] private readonly GameLoader _gameLoader;

    public bool IsUIActivated { get; set; }

    private void Awake()
    {
        _playerHealth.OnPlayerDies += DisableUI;
        _gameLoader.OnGameLoadingUI += SetScriptActiveState;
    }

    private void DisableUI()
    {
        if (IsUIActivated)
        {
            EnableDisableUI();
        }
        enabled = false;
    }

    private void SetScriptActiveState(bool activeState)
    {
        enabled = activeState;
    }

    public abstract void EnableDisableUI();

    private void OnDestroy()
    {
        _playerHealth.OnPlayerDies -= DisableUI;
        _gameLoader.OnGameLoadingUI -= SetScriptActiveState;
    }
}