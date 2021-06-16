using System;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] float m_sneakSpeed;
    [SerializeField] float m_walkSpeed;
    [SerializeField] float m_runSpeed;

    public Action OnPlayerRun { get; set; }
    public Action OnPlayerStoppedRun { get; set; }
    public Action OnPlayerStoppedSneak { get; set; }
    public Action OnPlayerSneak { get; set; }

    public float SlowDownFactor { get; set; }

    void Awake()
    {
        MainLinks.Instance.PlayerSpeed = this;
    }

    public float GetPlayerSpeed()
    {
        float movespeed = m_walkSpeed;

        if (Input.GetButton("Sneak"))
        {
            OnPlayerSneak.Invoke();
            movespeed = m_sneakSpeed;
        }
        else if (Input.GetButton("Sprint") && MainLinks.Instance.PlayerStamina.HasPlayerЕnoughStaminaToRun)
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

        return movespeed - SlowDownFactor;
    }
}
