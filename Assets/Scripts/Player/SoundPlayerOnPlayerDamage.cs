using UnityEngine;
using Zenject;

public class SoundPlayerOnPlayerDamage : SoundPlayerOnAction
{
    [Inject] PlayerHealthSystem m_playerHealth;

    protected override void SubscribeToAction()
    {
        m_playerHealth.OnPlayerGetsDamage += PlaySound;
    }

    protected override void UnsubscribeToAction()
    {
        m_playerHealth.OnPlayerGetsDamage -= PlaySound;
    }
}
