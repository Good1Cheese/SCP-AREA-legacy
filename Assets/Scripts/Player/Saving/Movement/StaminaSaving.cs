using UnityEngine;
using Zenject;

public class StaminaSaving : CoroutineUserSaving
{
    [Inject] private readonly PlayerStamina _playerStamina;

    public float curveTime;
    public float maxCurveTime;
    public bool isCoroutineGoing;

    protected override CoroutineWithDelayUser CoroutineWithDelayUser => _playerStamina;

    public override void Save()
    {
        curveTime = _playerStamina.CurveTime;
        maxCurveTime = _playerStamina.MaxCurveTime;
        isCoroutineGoing = _playerStamina.IsCoroutineGoing;
    }

    public override void LoadData()
    {
        _playerStamina.CurveTime = curveTime;
        _playerStamina.MaxCurveTime = maxCurveTime;

        if (!isCoroutineGoing) { return; }

        _playerStamina.StartWithoutInterrupt(new WaitForSeconds(currentDelay));
    }
}