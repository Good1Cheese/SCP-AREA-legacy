using UnityEngine;
using Zenject;

public class SettingsMenuControls : MonoBehaviour
{
    private SceneTransition _sceneTransition;

    [Inject]
    private void Construct(SceneTransition sceneTransition)
    {
        _sceneTransition = sceneTransition;
    }

    public void ReturnToMainMenu()
    {
        _sceneTransition.LoadScene((int)SceneTransition.Scenes.StartScene);
    }
}