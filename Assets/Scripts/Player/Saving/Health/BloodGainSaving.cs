using UnityEngine;
using Zenject;

public class BloodGainSaving : CoroutineUserSaving
{
    [Inject] private readonly BloodGain _bloodGain;

    public bool isActionGoing;
    public float gainPerSecond;

    protected override CoroutineWithDelayUser CoroutineWithDelayUser => _bloodGain;

    public override void Save()
    {
        isActionGoing = _bloodGain.IsCoroutineGoing;
        gainPerSecond = _bloodGain.GainPerSecond;
    }

    public override void LoadData()
    {
        if (!isActionGoing) { return; }

        _bloodGain.GainPerSecond = gainPerSecond;
        _bloodGain.StartWithoutInterrupt(new WaitForSeconds(currentDelay));
    }
}