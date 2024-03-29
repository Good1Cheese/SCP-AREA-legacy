﻿using Zenject;

public class SlowWalkEffectSaving : DataSaving
{
    [Inject] private readonly SlowWalkEffect _slowWalkEffect;

    public float slowWalkTime;

    public override void Save()
    {
        slowWalkTime = _slowWalkEffect.SlowWalkTime;
    }

    public override void Load()
    {
        _slowWalkEffect.SlowWalkTime = slowWalkTime;
        _slowWalkEffect.SetHeight(() => false, 1);
    }
}