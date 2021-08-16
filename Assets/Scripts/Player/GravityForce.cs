using UnityEngine;

public class GravityForce : MonoBehaviour
{
    [SerializeField] float m_initialVelocityValue;
    [SerializeField] float m_gravity;

    CharacterController m_characterController;
    Vector3 m_velocity;

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (m_characterController.isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = m_initialVelocityValue;
        }

        m_velocity.y += m_gravity * Time.fixedDeltaTime;
        m_characterController.Move(m_velocity * Time.fixedDeltaTime);
    }
}