public class SoundPlayerOnPlayerBleeding : SoundPlayerOnAction
{
    protected override void SubscribeToAction()
    {
        MainLinks.Instance.PlayerBleeding.OnPlayerBleeding += PlaySound;
    }

    protected override void UnsubscribeToAction()
    {
        MainLinks.Instance.PlayerBleeding.OnPlayerBleeding -= PlaySound;
    }
}
