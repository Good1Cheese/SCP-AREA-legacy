using UnityEngine;
using Zenject;

public class PickableItemHandler : ItemHandler, IClickable
{
    [SerializeField] protected PickableItem_SO m_pickableItem_SO;

    [Inject] protected readonly GameControllerInstaller m_gameControllerInstaller;

    [Inject] 
    void Construct(PlayerInstaller playerInstaller)
    {
        m_pickableItem_SO.GetDependencies(playerInstaller);
    }

    public void Clicked(int slotIndex)
    {
        if (m_pickableItem_SO.ShouldItemNotBeUsed()) { return; }
        m_pickableItem_SO.Use();
        m_gameControllerInstaller.PickableItemsInventory.RemoveItem(slotIndex);
    }

    public override Item_SO GetItem() => m_pickableItem_SO;

    public override void Equip()
    {
        m_gameControllerInstaller.PickableItemsInventory.AddItem(this);
    }

}
