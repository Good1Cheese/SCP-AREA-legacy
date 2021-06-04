using UnityEngine;

public class SettingsSceneController : MonoBehaviour
{
    public void ReturnToMainMenu()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.StartScene);
    }
}
