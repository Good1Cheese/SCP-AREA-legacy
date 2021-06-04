using UnityEngine;

[RequireComponent(typeof(RayProvider))]
public class InteractionProvider : MonoBehaviour
{
    [SerializeField] float _maxInteractionDistance;
    RayProvider _rayProvider;

    void Start()
    {
        _rayProvider = GetComponent<RayProvider>();
    }

    void Update()
    {
        Ray ray = _rayProvider.ProvideRay();
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxInteractionDistance))
        {
            bool isHitObjectInteractable = hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable);
            if (!isHitObjectInteractable) { return; }

            if (Input.GetButtonDown("Interaction"))
            {
                interactable.Interact();
            }
        }

    }
}
