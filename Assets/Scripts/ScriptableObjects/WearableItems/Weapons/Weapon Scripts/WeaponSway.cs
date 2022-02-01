using UnityEngine;
using Zenject;

public class WeaponSway : MonoBehaviour
{
    private PickableInventoryEnablerDisabler _pickableInventoryEnablerDisabler;
    private WeaponSlot _weaponSlot;

    [Inject]
    private void Inject(PickableInventoryEnablerDisabler pickableInventoryEnablerDisabler,
                        WeaponSlot weaponSlot)
    {
        _pickableInventoryEnablerDisabler = pickableInventoryEnablerDisabler;
        _weaponSlot = weaponSlot;
    }

    [SerializeField] private float _intensity;
    [SerializeField] private float _smooth;

    private Quaternion _originRotation;

    private void Awake()
    {
        _originRotation = transform.localRotation;
    }

    private void Update()
    {
        if (_pickableInventoryEnablerDisabler.IsActivated
            || _weaponSlot.ItemHandler == null) { return; }

        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        Quaternion xAngle = Quaternion.AngleAxis(-_intensity * mouseX, Vector3.up);
        Quaternion yAngle = Quaternion.AngleAxis(_intensity * mouseY, Vector3.right);
        Quaternion targetRotation = _originRotation * xAngle * yAngle;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _smooth);
    }
}