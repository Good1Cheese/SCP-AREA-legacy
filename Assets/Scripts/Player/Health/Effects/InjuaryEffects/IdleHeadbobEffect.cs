using UnityEngine;

public class IdleHeadbobEffect : InjuryEffect
{
    [SerializeField] private IdleCameraHeadBob _idleHeadbob;

    protected override float EffectValue
    {
        set => _idleHeadbob.CurveMultipliyer = value;
    }
}