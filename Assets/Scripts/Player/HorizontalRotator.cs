using UnityEngine;
using Zenject;

public class HorizontalRotator : MonoBehaviour
{
    [SerializeField] float m_ySensitivity;
    [SerializeField] float m_xSensitivity;
    [SerializeField] float m_verticalLookLimit;
    [SerializeField] Transform m_camera;
    [Inject] SettingsPresetInstaller m_settingsPresetInstaller;
    [Inject] PlayerInventory m_playerInventory;

    float m_startYSensitivity;
    Transform m_transform;
    float m_yRotation;

    void Start()
    {
        m_transform = transform;
        m_startYSensitivity = m_ySensitivity;
        Cursor.lockState = CursorLockMode.Locked;
        m_playerInventory.OnInvenoryButtonPressed += DisableRotation;
    }

    void Update()
    {
        RotateVertically();
        RotateHorizontally();
    }

    void RotateHorizontally()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * m_ySensitivity * m_settingsPresetInstaller.generalSensivity;

        m_yRotation -= mouseY;
        m_yRotation = Mathf.Clamp(m_yRotation, -m_verticalLookLimit, m_verticalLookLimit);

        m_camera.localRotation = Quaternion.Euler(m_yRotation, 0, 0);
    }

    void RotateVertically()
    {
        float xRotation = Input.GetAxis("Mouse X") * Time.deltaTime * m_xSensitivity * m_settingsPresetInstaller.generalSensivity;
        m_transform.Rotate(Vector3.up * xRotation);
    }

    void DisableRotation(bool isUIActivated)
    {
        if (isUIActivated)
        {
            m_settingsPresetInstaller.generalSensivity = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }
        m_settingsPresetInstaller.generalSensivity = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDestroy()
    {
        m_playerInventory.OnInvenoryButtonPressed -= DisableRotation;
    }
}
