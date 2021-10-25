using UnityEngine;

[CreateAssetMenu(fileName = "new Medkit", menuName = "ScriptableObjects/PickableItems/Medkit")]
public class Medkit_SO : PickableItem_SO, IHealthInjectable
{
    PlayerHealth m_playerHealth;
    CharacterBleeding m_playerBleeding;

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        base.GetDependencies(playerInstaller);
        m_playerHealth = playerInstaller.PlayerHealth;
        m_playerBleeding = playerInstaller.CharacterBleeding;
    }

    public override void Use()
    {
        m_playerBleeding.StopBleeding();
        m_playerHealth.Heal();
        m_playerHealth.Heal();
    }

    public override bool ShouldItemNotBeUsed()
    {
        return m_playerHealth.HealthCells.IsCurrentCellLast() && !m_playerBleeding.IsBleeding;
    }

    public void Inject()
    {
        throw new System.NotImplementedException();
    }
}
