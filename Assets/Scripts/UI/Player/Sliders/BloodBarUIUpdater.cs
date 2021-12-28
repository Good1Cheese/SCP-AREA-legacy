using Zenject;

public class BloodBarUIUpdater : StatisticsBarUIController
{
    [Inject] private readonly PlayerBlood _playerBlood;

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