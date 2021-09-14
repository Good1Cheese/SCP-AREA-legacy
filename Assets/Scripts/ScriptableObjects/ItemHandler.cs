using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemDataController))]
public class ItemHandler : IInteractable
{
    [SerializeField] Item_SO m_item_SO;

    public Item_SO Item_SO { get => m_item_SO; }
    public GameObject GameObject { get; set; }

    void Start()
    {
        GameObject = gameObject;
        m_item_SO.gameObject = GameObject;
    }

    [Inject]
    void Construct(PlayerInstaller playerInstaller)
    {
        m_item_SO.GetDependencies(playerInstaller);
    }

    public override void Interact()
    {
        m_item_SO.IsInInventory = true;
        GameObject.SetActive(false);
        m_item_SO.Equip();
    }

    void OnDestroy()
    {
        m_item_SO.OnDestroy();
    }
}
