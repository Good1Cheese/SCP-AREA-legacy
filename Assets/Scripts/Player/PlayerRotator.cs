using UnityEngine;
using Zenject;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float m_ySensitivity;
    [SerializeField] float m_xSensitivity;

    [SerializeField] float m_verticalLookLimit;

    [SerializeField] float m_smoothTime;

    [SerializeField] Transform m_camera;

    [Inject] readonly InventoryEnablerDisabler m_werableInventoryAcviteStateSetter;
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    float m_mouseY;
    float m_mouseX;

    public float YRotation { get; set; }
    public float XRotation { get; set; }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_werableInventoryAcviteStateSetter.OnInventoryButtonPressed += DisableRotation;
    }

    void Update()
    {
        RotateHorizontally();
        RotateVertically();
    }

    void RotateVertically()
    {
        m_mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * m_ySensitivity;

        YRotation -= m_mouseY;
        YRotation = Mathf.Clamp(YRotation, -m_verticalLookLimit, m_verticalLookLimit);

        Quaternion cameraTargetRotation = Quaternion.Euler(YRotation, 0, 0);
        m_camera.localRotation = Quaternion.Slerp(m_camera.localRotation, cameraTargetRotation, m_smoothTime * Time.deltaTime);
    }

    void RotateHorizontally()
    {
        m_mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * m_xSensitivity;

        XRotation += m_mouseX;

        Quaternion playerTargetRotation = Quaternion.Euler(0, XRotation, 0);
        m_playerTransform.localRotation = Quaternion.Slerp(m_playerTransform.localRotation, playerTargetRotation, m_smoothTime * Time.deltaTime);
    }

    void DisableRotation()
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
        enabled = !enabled;
    }

    void OnDestroy()
    {
        m_werableInventoryAcviteStateSetter.OnInventoryButtonPressed -= DisableRotation;
    }
}
