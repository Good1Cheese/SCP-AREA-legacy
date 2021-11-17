using Zenject;

public class ItemsInteractionInstaller : MonoInstaller
{
    private PickableItemsInteraction _pickableItemsInteraction;
    private WearableItemsInteraction _wearableItemsInteraction;

    public override void InstallBindings()
    {
        GetComponents();
        Container.BindInstance(_pickableItemsInteraction).AsSingle();
        Container.BindInstance(_wearableItemsInteraction).AsSingle();
    }

    private void GetComponents()
    {
        _pickableItemsInteraction = GetComponent<PickableItemsInteraction>();
        _wearableItemsInteraction = GetComponent<WearableItemsInteraction>();
    }
}
