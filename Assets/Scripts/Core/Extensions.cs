using UnityEngine;

public static class Extensions
{
    public static Keyframe GetLastKeyFrame(this AnimationCurve animationCurve)
    {
        return animationCurve[animationCurve.keys.Length - 1];
    }

    public static Keyframe GetFirstKeyFrame(this AnimationCurve animationCurve)
    {
        return animationCurve[0];
    }
}