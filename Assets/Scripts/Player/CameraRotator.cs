using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float ySensitivity;
    float yRotation;
    float lookLimit = 82.5f;

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * ySensitivity;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -lookLimit, lookLimit);

        transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
    }
}
