using UnityEngine;

[CreateAssetMenu(fileName = "new AdrenalinеInject", menuName = "ScriptableObjects/Adrenalinе Inject")]
public class AdrenalinInject_SO : PickableItem_SO
{
    [SerializeField] float m_adrenalineTime;

    PlayerHealth m_playerHealth;
    StaminaUseDisabler m_staminaUseDisabler;

    public override void GetDependencies(PlayerInstaller playerInstaller, GameControllerInstaller gameControllerInstaller)
    {
        base.GetDependencies(playerInstaller, gameControllerInstaller);
        m_playerHealth = playerInstaller.PlayerHealth;
        m_staminaUseDisabler = playerInstaller.StaminaUseDisabler;
    }

    public override void Use()
    {
        m_staminaUseDisabler.Disable(m_adrenalineTime);
        m_playerHealth.Heal();
    }
}
