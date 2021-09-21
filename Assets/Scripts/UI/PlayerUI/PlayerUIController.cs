using UnityEngine;
using Zenject;

public class PlayerUIController : MonoBehaviour
{
    [Inject] readonly PauseMenu m_pauseMenu;
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
