using Zenject;

public class MedkitHandler : InjectableItemHandler, IHealthInjectable
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly CharacterBleeding m_playerBleeding;

    bool m_isInjectUsed;

    public new void Inject()
    {
        m_isInjectUsed = true;
        Use();
    }

    public override void Use()
    {
        m_playerBleeding.StopBleeding();
        m_playerHealth.Heal(((Medkit_SO)GetItem()).healthToHeal);
    }

    public override void Clicked(int slotIndex)
    {
        if (m_isInjectUsed) { return; }

        base.Clicked(slotIndex);
        m_numOfUses -= m_numOfUses;
    }
}
