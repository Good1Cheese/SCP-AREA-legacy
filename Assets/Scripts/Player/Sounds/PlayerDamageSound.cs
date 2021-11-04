using Zenject;

public class PlayerDamageSound : SoundOnAction
{
    [Inject] readonly PlayerHealth m_playerHealth;

    protected override void SubscribeToAction()
    {
        m_playerHealth.OnPlayerGetsDamage += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        m_playerHealth.OnPlayerGetsDamage -= PlaySound;
    }
}
