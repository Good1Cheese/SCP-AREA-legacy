using UnityEngine;
using Zenject;

public class ItemSway : MonoBehaviour
{
    [SerializeField] private float _intensity;
    [SerializeField] private float _smooth;

    private PickableInventoryToggler _pickableInventoryToggler;
    private Quaternion _originRotation;

    [Inject]
    private void Inject(PickableInventoryToggler pickableInventoryToggler)
    {
        _pickableInventoryToggler = pickableInventoryToggler;
    }

    private void Awake()
    {
        _originRotation = transform.localRotation;
    }

    private void Start()
    {
        WearableSlot.CurrentItemActivatorChanged += EnableDisable;
        enabled = false;
    }

    public void EnableDisable(WearableItemActivator wearableItemActivator)
    {
        if (wearableItemActivator.IsActive)
        {
            enabled = true;
            return;
        }
        enabled = false;
    }

    private void Update()
    {
        if (_pickableInventoryToggler.IsToggled) { return; }

        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        Quaternion xAngle = Quaternion.AngleAxis(-_intensity * mouseX, Vector3.up);
        Quaternion yAngle = Quaternion.AngleAxis(_intensity * mouseY, Vector3.right);
        Quaternion targetRotation = _originRotation * xAngle * yAngle;

        transform.localRotation = Quaternion.Lerp(transform.localRotation,
                                                  targetRotation,
                                                  Time.deltaTime * _smooth);
    }

    private void OnDestroy()
    {
        WearableSlot.CurrentItemActivatorChanged -= EnableDisable;
    }
}