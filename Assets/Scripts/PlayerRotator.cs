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
        float xRotation = Input.GetAxis("Mouse X") * Time.deltaTime * xSensitivity;
        transform.Rotate(0, xRotation, 0);
    }
}
