using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float sneakSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    float moveSpeed;
    CharacterController characterController;
    PlayerStatus playerStatus;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        moveSpeed = walkSpeed;

        if (Input.GetButton("Sneak") && !playerStatus.IsRunning)
        {
            moveSpeed = sneakSpeed;
        }
        if (Input.GetButton("Sprint") && !playerStatus.IsSneaking)
        {
            moveSpeed = runSpeed;
        }

        Vector3 move = transform.right * horizontalMove + transform.forward * verticalMove;
        move = Vector3.ClampMagnitude(move, 1f) * Time.deltaTime;
        characterController.Move(move * moveSpeed);

        print(characterController.velocity.magnitude);
    }
}
