using UnityEngine;
using Zenject;

public class RespawnMenuController : MonoBehaviour
{
    [Inject] readonly SceneTransition m_sceneTransition;

    public void PlayAgain()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.ScpScene);
    }

    public void ReturnToMainMenu()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.StartScene);
    }
}
