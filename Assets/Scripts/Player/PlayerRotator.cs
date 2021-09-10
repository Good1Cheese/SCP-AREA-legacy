using UnityEngine;
using Zenject;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float m_ySensitivity;
    [SerializeField] float m_xSensitivity;
    [SerializeField] float m_verticalLookLimit;
    [SerializeField] Transform m_camera;
    [Inject] readonly PlayerInventory m_playerInventory;
    [Inject] readonly Transform m_playerTransform;

    float m_yRotation;

    public float m_mouseY { get; set; }
    public float m_mouseX { get; set; }

    void Start()
    {
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
        m_playerTransform.Rotate(Vector3.up * m_mouseX);
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
