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
            bool condition = ammo != null && ammo.Ammo_SO.ammoType == ammoHandler.Ammo_SO.ammoType;

            return condition;
        });

        if (secondAmmoHandler == null || secondAmmoHandler.Ammo + ammoHandler.Ammo > MAX_SLOT_AMMO) { return; }

        secondAmmoHandler.Ammo += ammoHandler.Ammo;
        ammoHandler.Ammo = 0;
    }
}
