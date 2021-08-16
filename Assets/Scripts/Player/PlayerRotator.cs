using UnityEngine;
using Zenject;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float m_ySensitivity;
    [SerializeField] float m_xSensitivity;
    [SerializeField] float m_verticalLookLimit;
    [SerializeField] Transform m_camera;
    [Inject] readonly PlayerInventory m_playerInventory;

    Transform m_transform;
    float m_yRotation;

    public float m_mouseY { get; set; }
    public float m_mouseX { get; set; }

    void Start()
    {
        m_transform = transform;
        Cursor.lockState = CursorLockMode.Locked;
        m_playerInventory.OnInventoryButtonPressed += DisableRotationAndMouse;
    }

    void Update()
    {
        RotateVertically();
        RotateHorizontally();
    }

    void RotateHorizontally()
    {
        m_mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * m_ySensitivity;

        m_yRotation -= m_mouseY;
        m_yRotation = Mathf.Clamp(m_yRotation, -m_verticalLookLimit, m_verticalLookLimit);

        m_camera.localRotation = Quaternion.Euler(m_yRotation, 0, 0);
    }

    void RotateVertically()
    {
        m_mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * m_xSensitivity;
        m_transform.Rotate(Vector3.up * m_mouseX);
    }

    void DisableRotationAndMouse()
    {
        Cursor.visible = enabled;
        enabled = !enabled;
        Cursor.lockState = (!enabled) ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryButtonPressed -= DisableRotationAndMouse;
    }
}
