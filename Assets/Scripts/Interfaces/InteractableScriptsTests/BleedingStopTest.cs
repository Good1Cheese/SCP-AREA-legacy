using UnityEngine;
using Zenject;

public class BleedingStopTest : Interactable
{
    private PlayerBlood _playerBlood;

    [Inject]
    private void Construct(PlayerBlood playerBlood)
    {
        _playerBlood = playerBlood;
    }

    public override void Interact()
    {
        _playerBlood.StopCoroutine();
    }
}