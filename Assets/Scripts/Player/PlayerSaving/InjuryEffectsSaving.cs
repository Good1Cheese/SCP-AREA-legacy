using Zenject;

public class InjuryEffectsSaving : DataSaving
{
    [Inject] private readonly InjuryEffectsController _injuryEffectsController;

    public float curveTargetTime;
    public float curveCurrentTime;

    public override void Save()
    {
        curveTargetTime = _injuryEffectsController.CurveTargetTime;
        curveCurrentTime = _injuryEffectsController.CurveCurrentTime;
    }

    public override void LoadData()
    {
        _injuryEffectsController.CurveTargetTime = curveTargetTime;
        _injuryEffectsController.CurveCurrentTime = curveCurrentTime;
        _injuryEffectsController.OnEffectTimeChanging?.Invoke(curveCurrentTime);
    }
}
