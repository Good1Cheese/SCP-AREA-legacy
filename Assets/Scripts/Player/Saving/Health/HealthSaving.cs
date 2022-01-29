using UnityEngine;
using Zenject;

public class HealthSaving : CoroutineUserSaving
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject] private readonly HealableHealth _healableHealth;

    public float healthAmount;
    public bool isCoroutineGoing;

    protected override CoroutineWithDelayUser CoroutineWithDelayUser => _healableHealth;

    public override void Save()
    {
        healthAmount = _playerHealth.Amount;
        isCoroutineGoing = _healableHealth.IsCoroutineGoing;
    }

    public override void Load()
    {
        _playerHealth.Amount = healthAmount;

        if (!isCoroutineGoing) { return; }

        _healableHealth.StartWithoutInterrupt(new WaitForSeconds(currentDelay));
    }
}