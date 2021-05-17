using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float xSensitivity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;    
    }

    void Update()
    {
        float xRotation = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xRotation);
    }
}
