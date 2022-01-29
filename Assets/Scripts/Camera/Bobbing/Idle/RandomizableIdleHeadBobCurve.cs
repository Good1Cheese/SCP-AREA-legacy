using System;

[Serializable]
public class RandomizableIdleHeadBobCurve : HeadboBCurve
{
    public float Min { get; set; }
    public float Max { get; set; }

    public void FindFirstAndLastPoints()
    {
        Min = curve.keys[0].value;
        Max = curve.GetLastKeyFrame().value;
    }

    public void Randomize()
    {
        for (int i = 1; i < curve.length - 1; i++)
        {
            float randomPosition = UnityEngine.Random.Range(Min, Max);
            curve.keys[i].value = randomPosition;
        }
    }
}