    using Zenject;

public class BandageHandler : PickableItemHandler
{
    [Inject] readonly CharacterBleeding m_playerBleeding;

    public override void Use()
    {
        m_playerBleeding.StopBleeding();
    }

    public override bool ShouldItemNotBeUsed => !m_playerBleeding.IsBleeding;
}
