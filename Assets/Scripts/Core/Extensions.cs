using UnityEngine;

public static class Extensions
{
    public static Keyframe GetLastKeyFrame(this AnimationCurve animationCurve)
    {
        return animationCurve.keys[animationCurve.keys.Length - 1];
    }
}