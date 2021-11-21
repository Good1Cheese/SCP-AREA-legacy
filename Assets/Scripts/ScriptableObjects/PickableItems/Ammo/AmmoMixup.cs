using System.Linq;
using UnityEngine;
using Zenject;

public class AmmoMixup : MonoBehaviour
{
    private const int MAX_SLOT_AMMO = 50;

    [Inject] readonly PickableItemsInventory _pickableItemsInventory;

    public void MixUpAmmo(AmmoHandler ammoHandler)
    {
        AmmoHandler secondAmmoHandler = (AmmoHandler)_pickableItemsInventory.Inventory.FirstOrDefault(item =>
        {
            AmmoHandler ammo = item as AmmoHandler;
            return ammo != null
                   && ammo.Ammo_SO.ammoType == ammoHandler.Ammo_SO.ammoType;
        });

        if (secondAmmoHandler == null || secondAmmoHandler.AmmoCount + ammoHandler.AmmoCount > MAX_SLOT_AMMO) { return; }

        secondAmmoHandler.AmmoCount += ammoHandler.AmmoCount;
        ammoHandler.AmmoCount = 0;
    }
}
