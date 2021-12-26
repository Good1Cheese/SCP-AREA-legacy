using System;
using System.Collections;
using UnityEngine;
using Zenject;

public abstract class DirectionLean : MonoBehaviour
{
    [SerializeField] private float _leanTimeSmoothing;
    [SerializeField] private float _leanTimeLimit;
    [SerializeField] private float _leanChangeTime;

    [SerializeField] private KeyCode _positivKey;
    [SerializeField] private KeyCode _negativKey;

    [SerializeField] protected float _leanSmoothing;
    [SerializeField] protected AnimationCurve _peekCurve;

    [Inject] readonly protected GameObjectTrigger _cameraTrigger;

    protected float _leanTime;
    protected float _topLeanTimeLimit;
    protected float _bottomLeanTimeLimit;

    public float LeanTime { set => _leanTime = value; }

    private void Start()
    {
        SetDefaultLeanLimit();

        _cameraTrigger.TriggerEnter += ReduceLeanStrenght;
        _cameraTrigger.TriggerStay += SetLeanLimit;
        _cameraTrigger.TriggerExit += SetDefaultLeanLimit;
    }

    private void ReduceLeanStrenght()
    {
        if (_leanTime == _leanTimeLimit || _leanTime == -_leanTimeLimit)
        {
            StartCoroutine(Cocoroutine(_leanTime / 2));
        }
    }

    private IEnumerator Cocoroutine(float targetLeanTime)
    {
        float elapsedTime = 0;

        while (elapsedTime < _leanChangeTime)
        {
            _leanTime = Mathf.Lerp(_leanTime, targetLeanTime, elapsedTime / _leanChangeTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private void SetLeanLimit()
    {
        if (_leanTime > 0)
        {
            _topLeanTimeLimit = _leanTime;
            return;
        }
        _bottomLeanTimeLimit = _leanTime;
    }

    protected void SetDefaultLeanLimit()
    {
        _topLeanTimeLimit = _leanTimeLimit;
        _bottomLeanTimeLimit = -_leanTimeLimit;
    }

    protected void GetLeanTime()
    {
        bool isPositivKeyPressed = Input.GetKey(_positivKey);
        bool isNegativKeyPressed = Input.GetKey(_negativKey);

        if (isPositivKeyPressed)
        {
            _leanTime += Time.deltaTime;
        }

        if (isNegativKeyPressed)
        {
            _leanTime -= Time.deltaTime;
        }

        if (!isPositivKeyPressed && !isNegativKeyPressed)
        {
            _leanTime = Mathf.Lerp(_leanTime, 0, _leanTimeSmoothing * Time.deltaTime);
        }

        _leanTime = Mathf.Clamp(_leanTime, _bottomLeanTimeLimit, _topLeanTimeLimit);
    }

    public abstract void Lean();
    public abstract void Restore();

    private void OnDestroy()
    {
        _cameraTrigger.TriggerEnter -= ReduceLeanStrenght;
        _cameraTrigger.TriggerStay -= SetLeanLimit;
        _cameraTrigger.TriggerExit -= SetDefaultLeanLimit;
    }
}