using UnityEngine;

public abstract class IdleCameraHeadBob : CameraHeadBob
{
    [SerializeField] protected RandomizableIdleHeadBobCurve xAxis;
    [SerializeField] protected RandomizableIdleHeadBobCurve yAxis;
    [SerializeField] protected RandomizableIdleHeadBobCurve zAxis;

    public float CurveMultipliyer { get; set; }

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
        return base.GetCurveValue(HeadBobCurve) * CurveMultipliyer;
    }

    protected abstract void ActivateHeadbob();
}