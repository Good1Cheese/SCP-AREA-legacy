using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float m_xSensitivity;
    Transform m_transform;

    void Start()
    {
        m_transform = transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float xRotation = Input.GetAxis("Mouse X") * m_xSensitivity * Time.deltaTime;
        m_transform.Rotate(Vector3.up * xRotation);
    }
}
