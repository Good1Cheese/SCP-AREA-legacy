using UnityEngine;

[CreateAssetMenu(fileName = "new Medkit", menuName = "ScriptableObjects/Medkit")]
public class Medkit_SO : PickableItem_SO
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
        m_playerHealth.Heal();
        m_playerBleeding.StopBleeding();
    }
}
