using UnityEngine;
using Zenject;

[RequireComponent(typeof(SceneTransition))]
public class MainInstaller : MonoInstaller
{
    SceneTransition m_sceneTransition;
    GameSaving m_gameSaving;
    GameLoading m_gameLoading;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_gameLoading).AsSingle();
        Container.BindInstance(m_gameSaving).AsSingle();
        Container.BindInstance(m_sceneTransition).AsSingle(); 
    }

    void GetComponents()
    {
        m_sceneTransition = GetComponent<SceneTransition>();
        m_gameLoading = GetComponent<GameLoading>();
        m_gameSaving = GetComponent<GameSaving>();
    }
}
