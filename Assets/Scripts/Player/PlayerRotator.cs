using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] float xSensitivity;

    void Start()
    {
        MainLinks.Instance.Player = gameObject;
        Cursor.lockState = CursorLockMode.Locked;    
    }

    void Update()
    {
        float xRotation = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xRotation);
    }
}
