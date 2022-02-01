using UnityEngine;
using Zenject;

public class BleedingTest : MonoBehaviour, IInteractable
{
    [SerializeField] private float _startBloodTimeLoss;

    private PlayerBlood _playerBlood;

    [Inject]
    private void Construct(PlayerBlood playerBlood)
    {
        _playerBlood = playerBlood;
    }

    public void Interact()
    {
        _playerBlood.StartBleeding(_startBloodTimeLoss, 1);
    }
}