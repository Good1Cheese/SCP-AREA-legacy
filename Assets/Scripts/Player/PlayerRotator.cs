using UnityEngine;
using Zenject;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float m_ySensitivity;
    [SerializeField] float m_xSensitivity;

    [SerializeField] float m_verticalLookLimit;

    [SerializeField] float m_smoothTime = 5;

    [SerializeField] Transform m_camera;

    [Inject] readonly WearableInventoryActivator m_inventoryAcviteStateSetter;
    [Inject] readonly Transform m_playerTransform;

    float m_yRotation;
    float m_xRotation;

    float m_mouseY;
    float m_mouseX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed += DisableRotationAndMouse;
    }

    void Update()
    {
        RotateHorizontally();
        RotateVertically();
    }

    void RotateVertically()
    {
        m_mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * m_ySensitivity;

        m_yRotation -= m_mouseY;
        m_yRotation = Mathf.Clamp(m_yRotation, -m_verticalLookLimit, m_verticalLookLimit);

        Quaternion cameraTargetRotation = Quaternion.Euler(m_yRotation, 0, 0);
        m_camera.localRotation = Quaternion.Slerp(m_camera.localRotation, cameraTargetRotation, m_smoothTime * Time.deltaTime);
    }

    void RotateHorizontally()
    {
        m_mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * m_xSensitivity;

        m_xRotation += m_mouseX;
        Quaternion plaeyerTargetRotation = Quaternion.Euler(0, m_xRotation, 0);

        m_playerTransform.localRotation = Quaternion.Slerp(m_playerTransform.localRotation, plaeyerTargetRotation, m_smoothTime * Time.deltaTime);
    }

    void DisableRotationAndMouse()
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
        enabled = !enabled;
    }

    void OnDestroy()
    {
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed -= DisableRotationAndMouse;
    }
}
