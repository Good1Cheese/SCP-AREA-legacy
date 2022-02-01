using UnityEngine;
using Zenject;

public class BleedingStopTest : MonoBehaviour, IInteractable
{
    private PlayerBlood _playerBlood;

    [Inject]
    private void Construct(PlayerBlood playerBlood)
    {
        _playerBlood = playerBlood;
    }

    public void Interact()
    {
        _playerBlood.StopCoroutine();
    }
}