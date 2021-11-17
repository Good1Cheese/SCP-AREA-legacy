using UnityEngine;

public class WearableItemsInventory : MonoBehaviour
{
    [SerializeField] private WearableItemSlot _keyCardSlot;
    [SerializeField] private WearableItemSlot _maskSlot;
    [SerializeField] private WeaponSlot _weaponSlot;
    [SerializeField] private WearableItemSlot _deviceSlot;
    [SerializeField] private InjectorSlot _injectorSlot;

    public WearableItemSlot KeyCardSlot => _keyCardSlot;
    public WearableItemSlot MaskSlot => _maskSlot;
    public WeaponSlot WeaponSlot => _weaponSlot;
    public WearableItemSlot UtilitySlot => _deviceSlot;
    public InjectorSlot InjectorSlot => _injectorSlot;

    private void Start()
    {
        if (_keyCardSlot == null
            || _weaponSlot == null
            || _maskSlot == null
            || _deviceSlot == null
            || InjectorSlot == null)
        {
            Debug.LogError("Fields aren't serialized", this);
        }
    }
}
