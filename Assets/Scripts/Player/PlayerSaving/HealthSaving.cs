using Zenject;

public class HealthSaving : DataSaving
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly InjuryEffectsController m_injuryEffectsController;

    public int healthAmount;

    public override void Save()
    {
        healthAmount = m_playerHealth.Amount;
    }

    public override void LoadData()
    {
        m_playerHealth.Amount = healthAmount;
        m_injuryEffectsController.SetCurveTimeDataAfterDamage();
    }
}
