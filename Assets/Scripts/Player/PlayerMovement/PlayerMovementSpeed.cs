using System;
using UnityEngine;
using Zenject;

public class PlayerMovementSpeed : MonoBehaviour
{
    [SerializeField] float m_sneakSpeed;
    [SerializeField] float m_walkSpeed;
    [SerializeField] float m_runSpeed;

    [Inject] PlayerStamina m_playerStamina;
    float slowDownFactor;

    public Action OnPlayerRun { get; set; }
    public Action OnPlayerStoppedRun { get; set; }
    public Action OnPlayerStoppedSneak { get; set; }
    public Action OnPlayerSneak { get; set; }

    public float GetPlayerSpeed()
    {
        float movespeed = m_walkSpeed;

        if (Input.GetButton("Sneak"))
        {
            OnPlayerSneak.Invoke();
            movespeed = m_sneakSpeed;
        }
        else if (Input.GetButton("Sprint") && m_playerStamina.StaminaValue > 0)
        {
            OnPlayerRun.Invoke();
            movespeed = m_runSpeed;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            OnPlayerStoppedRun.Invoke();
        }
        else if (Input.GetButtonUp("Sneak"))
        {
            OnPlayerStoppedSneak.Invoke();
        }

        return movespeed - slowDownFactor;
    }

    public void SlowDownSpeed(float slowDownFactor)
    {
        this.slowDownFactor = slowDownFactor;
    }
}
