using UnityEngine;
using Zenject;

public class DeathAnimationPlayer : MonoBehaviour
{
    [Inject] private readonly SceneTransition _sceneTransition;

    public void PlayDeathAnimation()
    {
        _sceneTransition.LoadScene((int)SceneTransition.Scenes.RespawnScene);
    }
}