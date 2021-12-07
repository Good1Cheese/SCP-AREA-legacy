using UnityEngine;
using Zenject;

[RequireComponent(typeof(SceneTransition))]
public class StartSceneInstaller : MonoInstaller
{
    private SceneTransition _sceneTransition;
    private GameSaving _gameSaving;
    private GameLoading _gameLoading;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(_gameLoading).AsSingle();
        Container.BindInstance(_gameSaving).AsSingle();
        Container.BindInstance(_sceneTransition).AsSingle();
    }

    private void GetComponents()
    {
        _sceneTransition = GetComponent<SceneTransition>();
        _gameLoading = GetComponent<GameLoading>();
        _gameSaving = GetComponent<GameSaving>();
    }
}
