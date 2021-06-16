using UnityEngine;

public class SoundPlayerOnPlayerDamage : SoundPlayerOnAction
{
    protected override void SubscribeToAction()
    {
        MainLinks.Instance.PlayerHealth.OnPlayerGetsDamage += PlaySound;
    }

    protected override void UnsubscribeToAction()
    {
        MainLinks.Instance.PlayerHealth.OnPlayerGetsDamage -= PlaySound;
    }
}
