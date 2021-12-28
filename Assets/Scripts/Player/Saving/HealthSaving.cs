using Zenject;

public class HealthSaving : DataSaving
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject] private readonly HealableHealth _healableHealth;

    public int healthAmount;
    public bool isHealActionGoing;

    public override void Save()
    {
        isHealActionGoing = _healableHealth.IsActionGoing;
        healthAmount = _playerHealth.Amount;
    }

    public override void LoadData()
    {
        _playerHealth.Amount = healthAmount;

        if (isHealActionGoing)
        {
            _healableHealth.StartActionWithInterrupt();
        }
    }
}

