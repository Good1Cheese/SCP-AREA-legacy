using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float _xSensitivity;
    Transform _transform;

    void Start()
    {
        _transform = transform;
        MainLinks.Instance.Player = gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float xRotation = Input.GetAxis("Mouse X") * _xSensitivity * Time.deltaTime;
        _transform.Rotate(Vector3.up * xRotation);
    }
}
