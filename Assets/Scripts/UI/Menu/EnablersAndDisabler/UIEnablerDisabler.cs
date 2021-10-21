using UnityEngine;
using Zenject;

public abstract class UIEnablerDisabler : MonoBehaviour
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly GameLoader m_gameLoader;

    public bool IsUIActivated { get; set; }

    void Awake()
    {
        m_playerHealth.OnPlayerDies += DisableUI;
        m_gameLoader.OnGameLoading += SetScriptActiveState;
    }

    void DisableUI()
    {
        if (IsUIActivated)
        {
            EnableDisableUI();
        }
        enabled = false;
    }

    void SetScriptActiveState(bool activeState)
    {
        enabled = activeState;
    }

    public abstract void EnableDisableUI();

    void OnDestroy()
    {
        m_playerHealth.OnPlayerDies -= DisableUI;
        m_gameLoader.OnGameLoading -= SetScriptActiveState;
    }
}