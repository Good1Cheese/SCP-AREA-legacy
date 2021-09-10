using Zenject;

public class GameControllerInstaller : MonoInstaller
{
    PauseMenu m_pauseMenu;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_pauseMenu).AsSingle();
    }

    void GetComponents()
    {
        m_pauseMenu = GetComponent<PauseMenu>();
    }
}