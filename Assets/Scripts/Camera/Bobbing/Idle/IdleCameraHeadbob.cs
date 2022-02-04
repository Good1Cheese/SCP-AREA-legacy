using UnityEngine;

public abstract class IdleCameraHeadBob : CameraHeadBob
{
    [SerializeField] protected RandomizableIdleHeadBobCurve xAxis;
    [SerializeField] protected RandomizableIdleHeadBobCurve yAxis;
    [SerializeField] protected RandomizableIdleHeadBobCurve zAxis;

    private float _curveTime;

    public float CurveMultipliyer { get; set; } = 1;
    protected override float СurveTime => _curveTime;

    public RandomizableIdleHeadBobCurve XAxis => xAxis;
    public RandomizableIdleHeadBobCurve YAxis => yAxis;
    public RandomizableIdleHeadBobCurve ZAxis => zAxis;

    private void Update()
    {
        _curveTime = Time.time;
        ActivateHeadbob();
    }

    public override float GetCurveValue(HeadboBCurve HeadBobCurve)
    {
        return HeadBobCurve.curve.Evaluate(_curveTime) * CurveMultipliyer;
    }

    protected abstract void ActivateHeadbob();
}