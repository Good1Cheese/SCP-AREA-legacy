using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(IdleHeadBobRandomize))]
public class IdleHeadBobRandomize : MonoBehaviour
{
    [SerializeField] private IdleCameraHeadBob _idleCameraHeadbob;
    [SerializeField] private float _delayBeforeRandomize;

    private WaitForSeconds _timeoutBeforeRandomize;
    private RandomizableIdleHeadBobCurve[] _idleHeadbobCurves;

    private void Awake()
    {
        _idleHeadbobCurves = new RandomizableIdleHeadBobCurve[]
        {
            _idleCameraHeadbob.XAxis,
            _idleCameraHeadbob.YAxis,
            _idleCameraHeadbob.ZAxis
        };

        ForEachIdleHeadBobCurve(curve => curve.FindFirstAndLastPoints());
        _timeoutBeforeRandomize = new WaitForSeconds(_delayBeforeRandomize);
        StartCoroutine(RandomizeCoroutine());
    }

    private IEnumerator RandomizeCoroutine()
    {
        while (true)
        {
            yield return _timeoutBeforeRandomize;
            ForEachIdleHeadBobCurve(curve => curve.Randomize());
        }
    }

    private void ForEachIdleHeadBobCurve(Action<RandomizableIdleHeadBobCurve> action)
    {
        for (int i = 0; i < _idleHeadbobCurves.Length; i++)
        {
            action(_idleHeadbobCurves[i]);
        }
    }
}