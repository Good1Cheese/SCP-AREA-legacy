using UnityEngine;

public class TestBloodingInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        MainLinks.Instance.PlayerHealthController.Scratch(10);
    }
}
