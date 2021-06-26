using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(MovementSpeed), typeof(PlayerStamina))]
public class PlayerMovement : MonoBehaviour
{
    MovementSpeed m_playerSpeed;
    CharacterController m_characterController;
    Transform m_transform;

    void Start()
    {
        m_transform = transform;
        m_playerSpeed = GetComponent<MovementSpeed>();
        m_characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove == 0 && verticalMove == 0) { return; }
        float moveSpeed = m_playerSpeed.GetPlayerSpeed();
 
        Vector3 move = m_transform.right * horizontalMove + m_transform.forward * verticalMove;
        move = Vector3.ClampMagnitude(move, 1f) * Time.deltaTime;
        m_characterController.Move(move * moveSpeed);
    }

}
