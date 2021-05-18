using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] float initialVelocityValue = -1f;
    CharacterController characterController;

    [SerializeField] float gravity;
    Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = initialVelocityValue;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}