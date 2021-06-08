using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float m_ySensitivity;
    [SerializeField] float m_verticalLookLimit;
    float m_yRotation;

    void Start()
    {
        MainLinks.Instance.Camera = transform;
    }

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * m_ySensitivity;

        m_yRotation -= mouseY;
        m_yRotation = Mathf.Clamp(m_yRotation, -m_verticalLookLimit, m_verticalLookLimit);

        MainLinks.Instance.Camera.localRotation = Quaternion.Euler(m_yRotation, 0, 0);
    }
}
