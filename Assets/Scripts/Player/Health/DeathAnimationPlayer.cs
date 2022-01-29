using UnityEngine;
using Zenject;

public class DeathAnimationPlayer : MonoBehaviour
{
    private SceneTransition _sceneTransition;

    [Inject]
    private void Construct(SceneTransition sceneTransition)
    {
        _sceneTransition = sceneTransition;
    }

    public void PlayDeathAnimation()
    {
        _sceneTransition.LoadScene((int)SceneTransition.Scenes.RespawnScene);
    }
}