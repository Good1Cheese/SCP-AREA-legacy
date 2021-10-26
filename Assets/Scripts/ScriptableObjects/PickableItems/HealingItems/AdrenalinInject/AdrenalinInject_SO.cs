using UnityEngine;

[CreateAssetMenu(fileName = "new AdrenalinеInject", menuName = "ScriptableObjects/PickableItems/Adrenalinе Inject")]
public class AdrenalinInject_SO : PickableItem_SO, IAdrenalinInjectable
{
    [SerializeField] int m_maxNumOfUses;
    [SerializeField] float m_adrenalineTime;

    PlayerHealth m_playerHealth;
    StaminaUseDisabler m_staminaUseDisabler;

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        base.GetDependencies(playerInstaller);
        m_playerHealth = playerInstaller.PlayerHealth;
        m_staminaUseDisabler = playerInstaller.StaminaUseDisabler;
    }

    public override void Use() { }

    public override bool ShouldItemNotBeUsed => true;

    public void Inject()
    {
        m_staminaUseDisabler.Disable(m_adrenalineTime);
        m_playerHealth.Heal();
    }

    public int NumOfUses => m_maxNumOfUses;
}
