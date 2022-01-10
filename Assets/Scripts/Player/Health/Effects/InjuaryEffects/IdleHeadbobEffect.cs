using UnityEngine;

public class IdleHeadbobEffect : InjuryEffect
{
    [SerializeField] private IdleCameraHeadbob _idleHeadbob;

    protected override float EffectValue
    {
        set => _idleHeadbob.CurveMultipliyer = value;
    }
}