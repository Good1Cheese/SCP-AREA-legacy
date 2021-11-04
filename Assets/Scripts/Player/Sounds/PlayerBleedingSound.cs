using Zenject;

public class PlayerBleedingSound : SoundOnAction
{
    [Inject] readonly CharacterBleeding m_playerBleeding;

    protected override void SubscribeToAction()
    {
        m_playerBleeding.OnPlayerBleeding += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        m_playerBleeding.OnPlayerBleeding -= PlaySound;
    }

}
