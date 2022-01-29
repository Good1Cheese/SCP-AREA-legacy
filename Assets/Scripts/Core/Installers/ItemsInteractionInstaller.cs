using Zenject;

public class ItemsInteractionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPickableItemsInteraction();
        BindWearableItemsInteraction();
    }

    private void BindPickableItemsInteraction()
    {
        Container.BindInstance(GetComponent<PickableItemsUse>())
            .AsSingle();

        Container.BindInstance(GetComponent<PickabeItemsDrop>())
            .AsSingle();
    }

    private void BindWearableItemsInteraction()
    {
        Container.BindInstance(GetComponent<WearableItemsUse>())
            .AsSingle();

        Container.BindInstance(GetComponent<WearableItemsDrop>())
            .AsSingle();
    }
}