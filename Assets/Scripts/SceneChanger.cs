using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    void Start()
    {
        MainLinks.Instance.SceneChanger = this;    
    }

    public enum Scenes
    {
        StartScene = 0,
        RespawnScene = 1, 
        ScpScene = 2
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}