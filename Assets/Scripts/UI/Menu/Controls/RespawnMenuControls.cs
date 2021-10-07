using UnityEngine;
using Zenject;

public class RespawnMenuControls : MonoBehaviour
{
    [Inject] readonly SceneTransition m_sceneTransition;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;    
    }

    public void ReturnToMainMenu()
    {
        m_sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.StartScene);
    }
}
