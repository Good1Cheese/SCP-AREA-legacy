using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float _ySensitivity;
    [SerializeField] float _verticalLookLimit;
    float _yRotation;

    void Start()
    {
        MainLinks.Instance.Camera = transform;
    }

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * _ySensitivity;

        _yRotation -= mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -_verticalLookLimit, _verticalLookLimit);

        MainLinks.Instance.Camera.localRotation = Quaternion.Euler(_yRotation, 0, 0);
    }
}
