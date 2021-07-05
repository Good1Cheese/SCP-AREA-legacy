using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CounterItemsInInventory : MonoBehaviour
{
    [Inject] readonly PlayerInventory m_playerInventory;

    TextMeshProUGUI m_textMesh;

    void Start()
    {   
        m_textMesh = GetComponent<TextMeshProUGUI>();
        m_playerInventory.OnInventoryChanged += UpdateItemsCount;
    }

    void UpdateItemsCount()
    {
        m_textMesh.text = string.Format($"{m_playerInventory.CurrentItemIndex}/{m_playerInventory.Inventory.Length}");
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryChanged -= UpdateItemsCount;
    }
}
    