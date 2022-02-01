using System;
using UnityEngine;

[Serializable]
public class InteractionTimeout
{
    [SerializeField] private float _delay;

    private WaitForSeconds _timeOut;

    public WaitForSeconds TimeOut => _timeOut;

    public void CreateTimeOut()
    {
        _timeOut = new WaitForSeconds(_delay);
    }
}