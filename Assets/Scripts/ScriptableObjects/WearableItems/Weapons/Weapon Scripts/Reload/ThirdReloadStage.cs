using UnityEngine;
using Zenject;

public class ThirdReloadStage : ReloadStage
{
    private WeaponReload _weaponReload;

    [Inject]
    private void Construct(WeaponReload weaponReload)
    {
        _weaponReload = weaponReload;
    }

    public override WaitForSeconds InteractionTimeout => _weaponHandler.Weapon_SO.thirdReloadStageTimeout;
    public override AudioClip Sound => _weaponHandler.Weapon_SO.thirdReloadStageSound;

    public override void Interact()
    {
        print("Third Stage");

        var nextClip = _weaponReload.NextClipAmmo;
        _weaponHandler.CurrentClipSlot.Set(nextClip.Item);
    }
}