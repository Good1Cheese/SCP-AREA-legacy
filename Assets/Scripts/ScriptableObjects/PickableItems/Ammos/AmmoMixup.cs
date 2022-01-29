using UnityEngine;
using Zenject;

public class AmmoMixup : MonoBehaviour
{
    private PickableItemsInventory _pickableItemsInventory;

    [Inject]
    private void Inject(PickableItemsInventory pickableItemsInventory)
    {
        _pickableItemsInventory = pickableItemsInventory;
    }

    public void MixUpAmmo(AmmoHandler ammoHandler)
    {
        AmmoHandler secondAmmoHandler = (AmmoHandler)_pickableItemsInventory.GetIem(item =>
        {
            AmmoHandler ammo = item as AmmoHandler;
            bool condition = ammo != null && ammo.Ammo_SO.ammoType == ammoHandler.Ammo_SO.ammoType;

            return condition;
        });

        if (secondAmmoHandler == null || secondAmmoHandler.Ammo + ammoHandler.Ammo > AmmoHandler.MAX_SLOT_AMMO) { return; }

        secondAmmoHandler.Ammo += ammoHandler.Ammo;
        ammoHandler.Ammo = 0;
    }
}