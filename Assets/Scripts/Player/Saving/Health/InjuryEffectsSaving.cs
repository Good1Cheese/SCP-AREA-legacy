using Zenject;

public class InjuryEffectsSaving : DataSaving
{
    [Inject] private readonly InjuryEffectsController _injuryEffectsController;

    public float curveTargetTime;
    public float curveCurrentTime;

    public override void Save()
    {
        curveTargetTime = _injuryEffectsController.CurveTargetTime;
        curveCurrentTime = _injuryEffectsController.CurveTime;
    }

    public override void Load()
    {
        _injuryEffectsController.CurveTargetTime = curveTargetTime;
        _injuryEffectsController.CurveTime = curveCurrentTime;
        _injuryEffectsController.CurveTimeChanged?.Invoke(curveCurrentTime);
    }
}
