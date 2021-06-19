using UnityEngine;
using Zenject;

public class SettingsSceneController : MonoBehaviour
{
    [Inject] SceneTransition m_sceneTransition;

    public void ReturnToMainMenu()
    {
        m_sceneTransition.ChangeScene((int)SceneTransition.Scenes.StartScene);
    }
}
