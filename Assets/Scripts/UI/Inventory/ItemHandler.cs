using UnityEngine;
using Zenject;

public class ItemHandler : IInteractable
{
    [SerializeField] Item_SO m_item_SO;

    public Item_SO Item_SO { get => m_item_SO; }

    [Inject]
    void Construct(PlayerInstaller playerInstaller)
    {
        m_item_SO.GetDependencies(playerInstaller);
    }

    public override void Interact()
    {
        m_item_SO.Equip();
        Destroy(gameObject);
    }
}
