using UnityEngine;
using Zenject;

[RequireComponent(typeof(SceneTransition))]
public class MainInstaller : MonoInstaller
{
    SceneTransition m_sceneTransition;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_sceneTransition);
    }

    void GetComponents()
    {
        m_sceneTransition = GetComponent<SceneTransition>();
    }
}