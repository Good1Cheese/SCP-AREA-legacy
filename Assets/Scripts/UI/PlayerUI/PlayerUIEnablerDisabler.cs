using UnityEngine;
using Zenject;

public class PlayerUIEnablerDisabler : MonoBehaviour
{
    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;
    GameObject m_gameObject;

    void Start()
    {
        m_gameObject = gameObject;
        m_pauseMenu.OnPauseMenuButtonPressed += SetActive;
    }

    void SetActive()
    {
        m_gameObject.SetActive(m_gameObject.activeSelf);
    }

    void OnDestroy()
    {
        m_pauseMenu.OnPauseMenuButtonPressed -= SetActive;
    }
}
