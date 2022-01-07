using System;
using UnityEngine;

[Serializable]
public class HeadbobCurve
{
    public AnimationCurve curve;

    public float Min { get; set; }
    public float Max { get; set; }

    public CameraHeadbob CameraHeadbob
    {
        set 
        {
            value.CurveChanged += FindFirstAndLastPoints;
            value.Randomized += Randomize;
        }
    }

    public void FindFirstAndLastPoints()
    {
        Min = curve.keys[0].value;
        Max = curve.GetLastKeyFrame().value;
    }

    public void Randomize()
    {
        for (int i = 1; i < curve.length - 1; i++)
        {
            curve.keys[i].value = UnityEngine.Random.Range(Min, Max);
        }
    }
}