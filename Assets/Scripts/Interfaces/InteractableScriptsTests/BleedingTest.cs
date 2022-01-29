using UnityEngine;
using Zenject;

public class BleedingTest : Interactable
{
    [SerializeField] private float _startBloodTimeLoss;

    private PlayerBlood _playerBlood;

    [Inject]
    private void Construct(PlayerBlood playerBlood)
    {
        _playerBlood = playerBlood;
    }

    public override void Interact()
    {
        _playerBlood.StartBleeding(_startBloodTimeLoss, 1);
    }
}