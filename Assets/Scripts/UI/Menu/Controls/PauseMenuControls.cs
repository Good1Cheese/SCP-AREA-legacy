using UnityEngine;
using Zenject;

public class PauseMenuControls : MonoBehaviour
{
    [Inject] readonly SceneTransition sceneTransition;
    [Inject] readonly GameSaving m_gameSaver;
    [Inject] readonly GameLoading m_gameLoader;
    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;

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
        m_pauseMenu.PauseOrUnpauseGame();
    }

    public void SaveGame()
    {
        m_pauseMenu.PauseOrUnpauseGame();
        m_gameSaver.Save();
    }

    public void LoadGame()
    {
        m_pauseMenu.PauseOrUnpauseGame();
        m_gameLoader.Load();
    }

    public void Exit()
    {
        sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.StartScene);
        Time.timeScale = 1;
    }

    void OnDestroy()
    {
        m_pauseMenu.OnPauseMenuButtonPressed -= ActivateOrDeacrivateUI;
    }
}
