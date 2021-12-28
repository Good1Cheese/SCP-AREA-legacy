using Zenject;

public class BloodSaving : DataSaving
{
    [Inject] private readonly PlayerBlood _playerBlood;

    public bool isBleedingAcitonGoing;
    public float curveTime;

    public override void Save()
    {
        curveTime = _playerBlood.CurveTime;
        isBleedingAcitonGoing = _playerBlood.IsActionGoing;
    }

    public override void LoadData()
    {
        _playerBlood.CurveTime = curveTime;

        if (!isBleedingAcitonGoing) { return; }

        _playerBlood.StartActionWithInterrupt();
    }
}