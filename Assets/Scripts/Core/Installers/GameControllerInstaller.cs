﻿using UnityEngine.Rendering;
using Zenject;

public class GameControllerInstaller : MonoInstaller
{
    PauseMenu m_pauseMenu;
    InjuryEffectsController m_injuryState;
    Volume m_volume;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_pauseMenu).AsSingle();
        Container.BindInstance(m_injuryState).AsSingle();
        Container.BindInstance(m_volume).AsSingle();
    }

    void GetComponents()
    {
        m_pauseMenu = GetComponent<PauseMenu>();
        m_injuryState = GetComponent<InjuryEffectsController>();
        m_volume = GetComponent<Volume>();
    }
}