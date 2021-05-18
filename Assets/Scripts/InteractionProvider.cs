using UnityEngine;

[RequireComponent(typeof(RayProvider))]
public class InteractionProvider : MonoBehaviour
{
    RayProvider rayProvider;
    [SerializeField] float maxInteractionDistance;

    void Start()
    {
        rayProvider = GetComponent<RayProvider>();
    }

    void Update()
    {
        Ray ray = rayProvider.ProvideRay();
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxInteractionDistance))
        {
            bool isHitObjectInteractable = hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable);
            if (isHitObjectInteractable)
            {
                if (Input.GetButton("Interaction"))
                {
                    interactable.Interact();
                }
            }
        }

    }
}
