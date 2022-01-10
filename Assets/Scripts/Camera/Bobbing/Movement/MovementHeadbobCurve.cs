using UnityEngine;
using System;

[Serializable]
public class MovementHeadbobCurve : HeadbobCurve 
{
    private Keyframe _firstkeyframe = new Keyframe();

    public void SetFirstPointValue(in float value)
    {
        _firstkeyframe.value = value;

        curve.MoveKey(0, _firstkeyframe);
    }
}