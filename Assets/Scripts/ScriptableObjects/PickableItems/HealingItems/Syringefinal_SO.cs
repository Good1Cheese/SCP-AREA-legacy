using UnityEngine;

[CreateAssetMenu(fileName = "new Syringefinal", menuName = "ScriptableObjects/Syringefinal")]
public class Syringefinal_SO : PickableItem_SO
{
    PlayerHealth playerHealth;

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        base.GetDependencies(playerInstaller);
        playerHealth = playerInstaller.PlayerHealth;
    }

    public override void Use()
    {
        playerHealth.Heal();
        playerHealth.Heal();
    }
}
