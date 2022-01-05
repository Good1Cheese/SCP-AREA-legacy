using UnityEngine;

public class IdleHeadbobEffect : InjuryEffect
{
    [SerializeField] private IdleHeadbob _idleHeadbob;

    protected override float EffectValue
    {
        set => _idleHeadbob.CurveMultipliyer = value;
    }
}