using Zenject;

public class BloodSaving : DataSaving
{
    [Inject] private readonly PlayerBlood _playerBlood;

    public bool isBleedingAcitonGoing;
    public float curveTime;
    public float lossMultipliyer;

    public override void Save()
    {
        curveTime = _playerBlood.CurveTime;
        lossMultipliyer = _playerBlood.LossMultipliyer;
        isBleedingAcitonGoing = _playerBlood.IsCoroutineGoing;
    }

    public override void LoadData()
    {
        _playerBlood.CurveTime = curveTime;

        if (!isBleedingAcitonGoing) { return; }

        _playerBlood.StartBleeding(0, lossMultipliyer);
    }
}