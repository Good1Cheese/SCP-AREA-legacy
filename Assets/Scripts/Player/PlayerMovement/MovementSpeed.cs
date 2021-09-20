using System;
using UnityEngine;
using Zenject;

public class MovementSpeed : MonoBehaviour
{
    [SerializeField] float m_sneakSpeed;
    [SerializeField] float m_walkSpeed;
    [SerializeField] float m_runSpeed;

    [Inject] readonly PlayerStamina m_playerStamina;

    float m_slowDownFactor;

    public Action OnPlayerRun { get; set; }
    public Action OnPlayerStoppedRun { get; set; }
    public Action OnPlayerStoppedSneak { get; set; }
    public Action OnPlayerSneak { get; set; }
    public Action OnPlayerWalks { get; set; }

    public bool IsPlayerRunning { get; set; }

    public float GetPlayerSpeed()
    {
        StopMovement();
        return GetSpecialSpeed();
    }

    float GetSpecialSpeed()
    {
        if (Input.GetButton("Sprint") && m_playerStamina.HasPlayerStamina)
        {
            OnPlayerRun?.Invoke();
            IsPlayerRunning = true;

            if (Input.GetButton("Sneak"))
            {
                OnPlayerStoppedSneak?.Invoke();
                IsPlayerRunning = false;
                return m_sneakSpeed - m_slowDownFactor;
            }

            return m_runSpeed - m_slowDownFactor;
        }
        else if (Input.GetButton("Sneak"))
        {
            OnPlayerSneak?.Invoke();

            return m_sneakSpeed - m_slowDownFactor;
        }

        OnPlayerWalks?.Invoke();
        return m_walkSpeed - m_slowDownFactor;
    }

    void StopMovement()
    {
        if (Input.GetButtonUp("Sprint"))
        {
            OnPlayerStoppedRun?.Invoke();
            IsPlayerRunning = false;
        }
        else if (Input.GetButtonUp("Sneak"))
        {
            OnPlayerStoppedSneak.Invoke();
        }
    }

    public void SlowDownSpeed(float slowDownFactor)
    {
        m_slowDownFactor = slowDownFactor;
    }
}
