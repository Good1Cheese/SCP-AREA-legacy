using Zenject;

public class GameControllerInstaller : MonoInstaller
{
    GameSaving GameSaving;
    GameLoading GameLoading;
    PauseMenu PauseMenu;
    EmptyDataHandler EmptyDataHandler;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(GameLoading).AsSingle();
        Container.BindInstance(GameSaving).AsSingle();
        Container.BindInstance(PauseMenu).AsSingle();
        Container.BindInstance(EmptyDataHandler).AsSingle();
    }

    void GetComponents()
    {
        GameLoading = GetComponent<GameLoading>();
        GameSaving = GetComponent<GameSaving>();
        PauseMenu = GetComponent<PauseMenu>();
        EmptyDataHandler = GetComponent<EmptyDataHandler>();
    }
}