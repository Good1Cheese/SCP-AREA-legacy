using Zenject;

public class StaminaDataSaving : DataSaving
{
    [Inject] readonly PlayerStamina m_playerStamina;

    public float stamina;

    public override void Save()
    {
        stamina = m_playerStamina.StaminaValue;
    }

    public override void Load()
    {
        m_playerStamina.StaminaValue = stamina;
        m_playerStamina.StopRegeneration();
        if (m_playerStamina.StaminaValue < m_playerStamina.MaxStaminaAmount)
        {
            m_playerStamina.Regenerate();
        }
    }

}
