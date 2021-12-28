using UnityEngine;
using Zenject;

public class BleedingTest : IInteractable
{
    [Inject] private readonly PlayerBlood _playerBleeding;

    [SerializeField] private float _startBloodTimeLoss;

    public override void Interact()
    {
        _playerBleeding.StartBleeding(_startBloodTimeLoss, 1);
    }
}