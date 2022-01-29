using UnityEngine;
using Zenject;

public class RespawnMenuControls : MonoBehaviour
{
    private SceneTransition _sceneTransition;

    [Inject]
    private void Construct(SceneTransition sceneTransition)
    {
        _sceneTransition = sceneTransition;
    }

    public void ReturnToMainMenu()
    {
        _sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.StartScene);

        Destroy(_sceneTransition.LoadingSceneUIController.gameObject);
    }
}