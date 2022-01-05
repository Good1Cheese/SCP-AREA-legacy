using Zenject;

public class HealthBarUIController : StatisticsBarUIController
{
    [Inject] private readonly PlayerHealth _playerHealth;

    private HealthBarUpdater _healthBarUpdater;

    protected new void Start()
    {
        base.Start();
        _healthBarUpdater = GetComponent<HealthBarUpdater>();
        _healthBarUpdater.HealthBarUIController = this;
    }

    protected override float GetValue()
    {
        return _playerHealth.Amount;
    }

    public override void UpdateUI()
    {
        _healthBarUpdater.UpdateCoroutine();
    }

    protected override void Subscribe()
    {
        _playerHealth.Changed += UpdateUI;
        _gameLoader.Loaded += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerHealth.Changed -= UpdateUI;
        _gameLoader.Loaded -= UpdateUI;
    }
}