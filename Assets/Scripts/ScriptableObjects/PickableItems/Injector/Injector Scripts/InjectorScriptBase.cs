using UnityEngine;

public abstract class InjectorScriptBase : MonoBehaviour
{
    [SerializeField] protected KeyCode m_key;

    public InventoryEnablerDisabler InventoryEnablerDisabler { get; set; }
    public WearableItemsInventory WearableItemsInventory { get; set; }
    public InjectorHandler InjectorHandler { get; set; }

    protected void Start()
    {
        InventoryEnablerDisabler.OnInventoryEnabledDisabled += SetActive;   
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key) || WearableItemsInventory.InjectorSlot.IsItemActionGoing) { return; }

        DoScriptAction();
    }

    protected abstract void DoScriptAction();

    void SetActive()
    {
        enabled = !enabled;
    }

    private void OnDestroy()
    {
        InventoryEnablerDisabler.OnInventoryEnabledDisabled -= SetActive;
    }
}
