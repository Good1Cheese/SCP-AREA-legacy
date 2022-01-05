using System;
using System.Collections;
using UnityEngine;

public class IdleHeadbob : MonoBehaviour
{
    [SerializeField] protected HeabBobCurve _xPosition;
    [SerializeField] protected HeabBobCurve _yPosition;
    [SerializeField] protected HeabBobCurve _zPosition;
    [SerializeField] private float _delayBeforeRandomize;

    protected float _curveTime;
    private float _curveValueMultipliyer = 1;
    WaitForSeconds _timeoutBeforeRandomize;

    public float CurveMultipliyer { set => _curveValueMultipliyer = value; }

    private void Start()
    {
        FindFirstAndLastPoints(_xPosition);
        FindFirstAndLastPoints(_yPosition);
        FindFirstAndLastPoints(_zPosition);
        _timeoutBeforeRandomize = new WaitForSeconds(_delayBeforeRandomize);
        StartCoroutine(RandomizeHeadbob());
    }

    private void FindFirstAndLastPoints(HeabBobCurve heabBobCurve)
    {
        heabBobCurve.Min = heabBobCurve.curve.keys[0].value;
        heabBobCurve.Max = heabBobCurve.curve.GetLastKeyFrame().value;
    }

    private IEnumerator RandomizeHeadbob()
    {
        yield return _timeoutBeforeRandomize;

        RandomizeCurve(_xPosition);
        RandomizeCurve(_yPosition);
        RandomizeCurve(_zPosition);
    }

    private void RandomizeCurve(HeabBobCurve HeabBobCurve)
    {
        for (int i = 1; i < HeabBobCurve.curve.length - 1; i++)
        {
            HeabBobCurve.curve.keys[i].value = UnityEngine.Random.Range(HeabBobCurve.Min, HeabBobCurve.Max);
        }
    }

    public float GetCurveValue(HeabBobCurve HeabBobCurve)
    {
        return HeabBobCurve.curve.Evaluate(_curveTime) * _curveValueMultipliyer;
    }
}

[Serializable]
public class HeabBobCurve
{
    public AnimationCurve curve;

    public float Min { get; set; }
    public float Max { get; set; }
}