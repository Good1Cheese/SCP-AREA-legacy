using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    CharacterController characterController;

    public bool IsAlive;
    public bool IsGrounded;
    public bool IsRunning;
    public bool IsSneaking;
    public bool CanMove;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        CanMove = true;
    }
}
