using Zenject;

public class BloodBarUIUpdater : StatisticsBarUIController
{
    private PlayerBlood _playerBlood;

    [Inject]
    private void Construct(PlayerBlood playerBlood)
    {
        _playerBlood = playerBlood;
    }

    protected override float GetValue()
    {
        return _playerBlood.Amount;
    }

    protected override void Subscribe()
    {
        _playerBlood.Changed += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerBlood.Changed -= UpdateUI;
    }
}