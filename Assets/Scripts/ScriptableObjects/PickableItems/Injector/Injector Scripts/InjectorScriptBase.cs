using UnityEngine;

public abstract class InjectorScriptBase : MonoBehaviour
{
    [SerializeField] protected KeyCode _key;

    public InventoryEnablerDisabler InventoryEnablerDisabler { get; set; }
    public InjectorSlot InjectorSlot { get; set; }
    public InjectorHandler InjectorHandler { get; set; }

    protected void Start()
    {
        InventoryEnablerDisabler.OnInventoryEnabledDisabled += SetActive;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(_key) || InjectorSlot.IsItemActionGoing) { return; }

        DoScriptAction();
    }

    protected abstract void DoScriptAction();

    private void SetActive()
    {
        enabled = !enabled;
    }

    private void OnDestroy()
    {
        InventoryEnablerDisabler.OnInventoryEnabledDisabled -= SetActive;
    }
}
