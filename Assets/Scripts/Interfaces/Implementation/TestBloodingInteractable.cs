using UnityEngine;

public class TestBloodingInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        MainLinks.Instance.PlayerHealth.Scratch(10);
    }
}
