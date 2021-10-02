using UnityEngine;
using Zenject;

public class RespawnMenuControls : MonoBehaviour
{
    [Inject] readonly SceneTransition m_sceneTransition;
    [Inject] readonly GameSaving m_gameSaver;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;    
    }

    public void PlayAgain()
    {
        m_gameSaver.SaveData.Clear();
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.ScpScene);
    }

    public void ReturnToMainMenu()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.StartScene);
    }
}
