using UnityEngine;
using Zenject;

public class PickableItemHandler : ItemHandler
{
    [SerializeField] protected PickableItem_SO m_pickableItem_SO;

    protected PlayerInstaller m_playerInstaller;
    protected GameControllerInstaller m_gameControllerInstaller;

    [Inject] 
    void Construct(PlayerInstaller playerInstaller, GameControllerInstaller gameControllerInstaller)
    {
        m_playerInstaller = playerInstaller;
        m_gameControllerInstaller = gameControllerInstaller;
        m_pickableItem_SO.GetDependencies(playerInstaller, gameControllerInstaller);
    }

    public override Item_SO GetItem() => m_pickableItem_SO;

    public override void Equip()
    {
        m_gameControllerInstaller.PickableItemsInventory.AddItem(this);
    }

    public virtual void OnItemClicked(int slotIndex)
    {
        if (m_pickableItem_SO.ShouldItemNotUsed()) { return; }
        m_pickableItem_SO.Use();
        m_gameControllerInstaller.PickableItemsInventory.RemoveItem(slotIndex);
    }
}
