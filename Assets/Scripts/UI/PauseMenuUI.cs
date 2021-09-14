using UnityEngine;
using Zenject;

public class PauseMenuUI : MonoBehaviour
{
    [Inject] readonly SceneTransition sceneTransition;
    [Inject] readonly GameSaving m_gameSaver;
    [Inject] readonly GameLoading m_gameLoader;
    [Inject] readonly PauseMenu m_pauseMenu;

    GameObject m_gameObject;

    void Start()
    {
        m_gameObject = gameObject;
        m_gameObject.SetActive(false);
        m_pauseMenu.OnPauseMenuButtonPressed += ActivateOrDeacrivateUI;
    }

    public void ActivateOrDeacrivateUI()
    {
        Time.timeScale = (m_gameObject.activeSelf) ? 1 : 0;
        m_gameObject.SetActive(!m_gameObject.activeSelf);
    }

    public void UnpauseGame()
    {
        m_pauseMenu.PauseGameOrUnpauseGame();
    }

    public void SaveGame()
    {
        m_pauseMenu.PauseGameOrUnpauseGame();
        m_gameSaver.Save();
    }

    public void LoadGame()
    {
        m_pauseMenu.PauseGameOrUnpauseGame();
        m_gameLoader.Load();
    }

    public void Exit()
    {
        sceneTransition.LoadScene((int)SceneTransition.Scenes.StartScene);
    }

    void OnDestroy()
    {
        m_pauseMenu.OnPauseMenuButtonPressed -= ActivateOrDeacrivateUI;
    }
}
