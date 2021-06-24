using UnityEngine;

public class TestBloodingInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerHealthSystem a;
    public void Interact()
    {
        a.Damage();
    }
}
