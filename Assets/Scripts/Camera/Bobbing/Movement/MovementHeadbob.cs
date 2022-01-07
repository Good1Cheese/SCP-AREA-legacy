using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MovementHeadbob : IdlePositionHeadbob
{
    [SerializeField] private int _positionReturnSmoothing;
    [SerializeField] private float _targetTime;

    [Inject] private readonly Camera _mainCamera;

    private bool _isCoroutineGoing;
    private Vector3 _zero = Vector3.zero;

    protected new void Awake()
    {
        _transform = _mainCamera.transform;
    }

    protected new void Update()
    {
        if (_isCoroutineGoing) { return; }

        _transform.localPosition = Vector3.Lerp(_transform.localPosition, _zero, _positionReturnSmoothing * Time.deltaTime);
    }

    protected override void ActivateHeadbob()
    {
        StartCoroutine(ActivateHeadbobCoroutine());
    }

    private IEnumerator ActivateHeadbobCoroutine()
    {
        _isCoroutineGoing = true;
        while (_curveTime < _targetTime)
        {
            base.ActivateHeadbob();
            yield return null;
        }

        Randomized?.Invoke();
        _isCoroutineGoing = false;
        _curveTime = 0;
    }
}