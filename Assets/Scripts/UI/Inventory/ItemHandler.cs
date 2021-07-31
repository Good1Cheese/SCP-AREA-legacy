using UnityEngine;
using Zenject;

public class ItemHandler : IInteractable
{
    [SerializeField] Item_SO m_item_SO;

    GameObject m_gameObject;

    public Item_SO Item_SO { get => m_item_SO; }

    void Start()
    {
        m_gameObject = gameObject;
    }

    [Inject]
    void Construct(PlayerInstaller playerInstaller)
    {
        m_item_SO.GetDependencies(playerInstaller);
    }

    public override void Interact()
    {
        m_item_SO.Equip();
        m_gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        m_item_SO.OnDestroy();
    }
}
