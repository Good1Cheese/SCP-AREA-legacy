using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        characterController.Move(transform.forward * verticalMove);
        characterController.Move(transform.right * horizontalMove);
    }
}
