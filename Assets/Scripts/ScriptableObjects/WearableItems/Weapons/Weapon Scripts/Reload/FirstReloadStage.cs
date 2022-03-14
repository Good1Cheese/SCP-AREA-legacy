using UnityEngine;

public class FirstReloadStage : ReloadStage
{
    public override WaitForSeconds InteractionTimeout => _weaponHandler.Weapon_SO.firstReloadStageTimeout;
    public override AudioClip Sound => _weaponHandler.Weapon_SO.firstReloadStageSound;

    public override void Interact()
    {
        print("First Stage");
    }
}