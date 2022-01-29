using UnityEngine;
using System;

[Serializable]
public class MovementHeadBobCurve : HeadboBCurve 
{
    private Keyframe _keyFrameForContinue = new Keyframe();

    public void SetFirstPointValue(in float value)
    {
        _keyFrameForContinue.value = value;
        curve.MoveKey(0, _keyFrameForContinue);
    }
}