public abstract class ItemsInteraction : UIInteractable
{
    protected ItemSlot _itemSlot;

    public void CallFunction(ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        TryInteract();
    }
}