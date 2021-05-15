using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float ySensitivity;
    float yRotation;

    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * ySensitivity;
        yRotation -= vertical;

        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
    }
}
