using UnityEngine;

public class TestBloodingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerHealth a;
    public void Interact()
    {
        a.Scratch(10);
    }
}
