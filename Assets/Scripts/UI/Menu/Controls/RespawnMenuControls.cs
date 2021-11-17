using UnityEngine;
using Zenject;

public class RespawnMenuControls : MonoBehaviour
{
    [Inject] private readonly SceneTransition _sceneTransition;

    private void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToMainMenu()
    {
        _sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.StartScene);

        Destroy(_sceneTransition.LoadingSceneUIController.gameObject);
    }
}
