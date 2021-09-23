using UnityEngine;
using Zenject;

public class PickableItemHandler : ItemHandler
{
    [SerializeField] protected PickableItem_SO m_pickableItem_SO;

    PlayerInstaller m_playerInstaller;

    [Inject] 
    void Construct(PlayerInstaller playerInstaller)
    {
        m_playerInstaller = playerInstaller;
        m_pickableItem_SO.GetDependencies(playerInstaller);
    }

    public override Item_SO GetItem() => m_pickableItem_SO;

    public override void Equip()
    {
        m_playerInstaller.PlayerInventory.AddItem(this);
    }

    public virtual void OnItemClicked(int slotIndex)
    {
        if (m_pickableItem_SO.ShouldItemNotUsed()) { return; }
        m_pickableItem_SO.Use();
        m_playerInstaller.PlayerInventory.RemoveItem(slotIndex);
    }
}
