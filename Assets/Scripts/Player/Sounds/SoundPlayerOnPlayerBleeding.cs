using Zenject;

public class SoundPlayerOnPlayerBleeding : SoundPlayerOnAction
{
    [Inject] CharacterBleeding m_playerBleeding;

    protected override void SubscribeToAction()
    {
        m_playerBleeding.OnPlayerBleeding += PlaySound;
    }

    protected override void UnsubscribeToAction()
    {
        m_playerBleeding.OnPlayerBleeding -= PlaySound;
    }

}
