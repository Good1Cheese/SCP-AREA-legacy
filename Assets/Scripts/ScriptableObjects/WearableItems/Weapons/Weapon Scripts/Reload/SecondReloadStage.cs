using UnityEngine;

public class SecondReloadStage : ReloadStage
{
    public override WaitForSeconds InteractionTimeout => _weaponHandler.Weapon_SO.secondReloadStageTimeout;
    public override AudioClip Sound => _weaponHandler.Weapon_SO.secondReloadStageSound;

    public override void Interact()
    {
        print("Second Stage");
    }
}