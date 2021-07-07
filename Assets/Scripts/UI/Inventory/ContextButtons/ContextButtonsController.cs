using UnityEngine;
using Zenject;

public abstract class ContextButtonsController : MonoBehaviour
{
    [SerializeField] protected Vector2 m_offset;

    [Inject] public PlayerInventory PlayerInventory { get; }
    public InventorySlot CurrentCell { get; set; }
    public Transform Transform { get; set; }
    public GameObject GameObject { get; set; }

    public abstract void ActiveteOnAction();
    public abstract void DeactiveteOnAction();

    void Start()
    {
        Transform = transform;
        GameObject = gameObject;
        GameObject.SetActive(false);
        ActiveteOnAction();
    }

    public void ActivateContextButtons(Vector2 position, InventorySlot inventoryCell)
    {
        CurrentCell = inventoryCell;
        GameObject.SetActive(true);
        Transform.position = position + m_offset;
    }

    void OnDestroy()
    {
        DeactiveteOnAction();
    }

}
