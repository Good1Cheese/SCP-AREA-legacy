using UnityEngine;
using Zenject;

public class SettingsMenuControls : MonoBehaviour
{
    [Inject] private readonly SceneTransition _sceneTransition;

    public void ReturnToMainMenu()
    {
        _sceneTransition.LoadScene((int)SceneTransition.Scenes.StartScene);
    }
}
