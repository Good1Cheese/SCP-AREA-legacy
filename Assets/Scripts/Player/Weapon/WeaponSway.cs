using UnityEngine;
using Zenject;

public class WeaponSway : WeaponAction
{
    [SerializeField] float m_intensity;
    [SerializeField] float m_smooth;

    Quaternion m_originRotation;
    Transform m_transform;

    void Start()
    {
        m_transform = transform;
        m_originRotation = m_transform.localRotation;
        enabled = false;
    }

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        var xAngle = Quaternion.AngleAxis(-m_intensity * mouseX, Vector3.up);
        var yAngle = Quaternion.AngleAxis(m_intensity * mouseY, Vector3.right);
        Quaternion targetRotation = m_originRotation * xAngle * yAngle;   

        m_transform.localRotation = Quaternion.Lerp(m_transform.localRotation, targetRotation, Time.deltaTime * m_smooth);
    }
}
