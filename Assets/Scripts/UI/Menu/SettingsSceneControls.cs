using UnityEngine;
using Zenject;

public class SettingsSceneControls : MonoBehaviour
{
    [Inject] readonly SceneTransition m_sceneTransition;

    public void ReturnToMainMenu()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.StartScene);
    }
}