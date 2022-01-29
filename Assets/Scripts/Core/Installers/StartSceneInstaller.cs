using UnityEngine;
using Zenject;

[RequireComponent(typeof(SceneTransition))]
public class StartSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGameSaving();
        BindSceneTransition();
    }

    private void BindGameSaving()
    {
        Container.BindInstance(GetComponent<GameLoading>()).AsSingle();
        Container.BindInstance(GetComponent<GameSaving>()).AsSingle();
    }

    private void BindSceneTransition()
    {
        Container.BindInstance(GetComponent<SceneTransition>()).AsSingle();
    }
}