using Zenject;

public class HealthBarUIController : StatisticsBarUIController
{
    private PlayerHealth _playerHealth;
    private HealthBarUpdater _healthBarUpdater;

    [Inject]
    private void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Awake()
    {
        _healthBarUpdater = GetComponent<HealthBarUpdater>();
        _healthBarUpdater.HealthBarUIController = this;
    }

    protected override float GetValue()
    {
        return _playerHealth.Amount;
    }

    public override void UpdateUI()
    {
        _healthBarUpdater.InvokeCoroutine();
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