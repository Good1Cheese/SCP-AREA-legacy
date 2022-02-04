using UnityEngine;
using Zenject;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float _ySensitivity;
    [SerializeField] private float _xSensitivity;

    [SerializeField] private float _verticalLookLimit;
    [SerializeField] private float _smoothTime;

    private Transform _mainCamera;
    private PickableInventoryToggler _pickableInventoryToggler;
    private Transform _playerTransform;

    private float _mouseY;
    private float _mouseX;

    public float YRotation { get; set; }
    public float XRotation { get; set; }

    [Inject]
    private void Construct([Inject(Id = "Camera")] Transform mainCamera,
                           PickableInventoryToggler pickableInventoryToggler,
                           [Inject(Id = "Player")] Transform playerTransform)
    {
        _mainCamera = mainCamera;
        _pickableInventoryToggler = pickableInventoryToggler;
        _playerTransform = playerTransform;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pickableInventoryToggler.Toggled += DisableRotation;
    }

    private void Update()
    {
        RotateHorizontally();
        RotateVertically();
    }

    private void RotateVertically()
    {
        _mouseY = Input.GetAxis("Mouse Y") * Time.fixedDeltaTime * _ySensitivity;

        YRotation -= _mouseY;
        YRotation = Mathf.Clamp(YRotation, -_verticalLookLimit, _verticalLookLimit);

        Quaternion cameraTargetRotation = Quaternion.Euler(YRotation, 0, 0);
        _mainCamera.localRotation = Quaternion.Slerp(_mainCamera.localRotation, cameraTargetRotation, _smoothTime * Time.fixedDeltaTime);
    }

    private void RotateHorizontally()
    {
        _mouseX = Input.GetAxis("Mouse X") * Time.fixedDeltaTime * _xSensitivity;

        XRotation += _mouseX;

        Quaternion playerTargetRotation = Quaternion.Euler(0, XRotation, 0);
        _playerTransform.localRotation = Quaternion.Slerp(_playerTransform.localRotation, playerTargetRotation, _smoothTime * Time.fixedDeltaTime);
    }

    private void DisableRotation()
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
        enabled = !enabled;
    }

    private void OnDestroy()
    {
        _pickableInventoryToggler.Toggled -= DisableRotation;
    }
}