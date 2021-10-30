using UnityEngine;

public class InjectorScriptBase : MonoBehaviour
{
    [SerializeField] protected KeyCode m_key;

    public InventoryEnablerDisabler InventoryEnablerDisabler { get; set; }

    protected void Start()
    {
        InventoryEnablerDisabler.OnInventoryEnabledDisabled += SetActive;   
    }

    void SetActive()
    {
        enabled = !enabled;
    }

    private void OnDestroy()
    {
        InventoryEnablerDisabler.OnInventoryEnabledDisabled -= SetActive;
    }
}
