using Zenject;

public class ItemsInteractionInstaller : MonoInstaller
{
    PickableItemsInteraction m_pickableItemsInteraction;
    WearableItemsInteraction m_wearableItemsInteraction;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(m_pickableItemsInteraction).AsSingle();
        Container.BindInstance(m_wearableItemsInteraction).AsSingle();
    }

    void GetComponents()
    {
        m_pickableItemsInteraction = GetComponent<PickableItemsInteraction>();
        m_wearableItemsInteraction = GetComponent<WearableItemsInteraction>();
    }
}
