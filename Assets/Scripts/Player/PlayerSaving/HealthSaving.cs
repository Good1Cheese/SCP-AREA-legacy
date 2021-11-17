using Zenject;

public class HealthSaving : DataSaving
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject] private readonly HealableHealth _healableHealth;

    public int healthAmount;
    public bool isHealGoing;

    public override void Save()
    {
        isHealGoing = _healableHealth.IsHealGoing;
        healthAmount = _playerHealth.Amount;
    }

    public override void LoadData()
    {
        _playerHealth.Amount = healthAmount;

        if (isHealGoing)
        {
            _healableHealth.StartHeal();
        }
    }
}

