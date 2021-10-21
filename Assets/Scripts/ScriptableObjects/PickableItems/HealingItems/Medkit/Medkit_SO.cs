using UnityEngine;

[CreateAssetMenu(fileName = "new Medkit", menuName = "ScriptableObjects/Medkit")]
public class Medkit_SO : PickableItem_SO
{
    PlayerHealth m_playerHealth;
    CharacterBleeding m_playerBleeding;

    public override void GetDependencies(PlayerInstaller playerInstaller, GameControllerInstaller gameControllerInstaller)
    {
        base.GetDependencies(playerInstaller, gameControllerInstaller);
        m_playerHealth = playerInstaller.PlayerHealth;
        m_playerBleeding = playerInstaller.CharacterBleeding;
    }

    public override void Use()
    {
        m_playerBleeding.StopBleeding();
        m_playerHealth.Heal();
        m_playerHealth.Heal();
    }

    public override bool ShouldItemNotBeUsed() => m_playerHealth.HealthCells.IsCurrentCellLast() && !m_playerBleeding.IsBleeding;
}
