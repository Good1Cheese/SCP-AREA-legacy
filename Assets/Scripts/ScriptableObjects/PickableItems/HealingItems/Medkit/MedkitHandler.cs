using Zenject;

public class MedkitHandler : InjectableItemHandler, IHealthInjectable
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly CharacterBleeding m_playerBleeding;

    bool m_isInjectUsed;
    HealthCellHealEffect m_healthCellHealEffect;

    new void Start()
    {
        base.Start();

        AutoHealableHealthCell autoHealableHealthCell = (AutoHealableHealthCell)m_playerHealth.HealthCells.Cells[m_playerHealth.HealthCells.Cells.Count - 1];
        m_healthCellHealEffect = autoHealableHealthCell.HealthCellHealEffect;
    }

    public new void Inject()
    {
        m_isInjectUsed = true;
        Use();
    }

    public override void Use()
    {
        m_playerBleeding.StopBleeding();
        m_playerHealth.Heal();
    }

    public override void Clicked(int slotIndex)
    {
        if (m_isInjectUsed) { return; }

        base.Clicked(slotIndex);
        m_numOfUses -= m_numOfUses;
    }

    public override bool ShouldItemNotBeUsed
    {
        get
        {
            print(!m_playerHealth.HealthCells.IsCurrentCellLast());
            print(m_healthCellHealEffect.IsHealing);
            return !m_playerHealth.HealthCells.IsCurrentCellLast() || m_healthCellHealEffect.IsHealing;
        }
    }
}
