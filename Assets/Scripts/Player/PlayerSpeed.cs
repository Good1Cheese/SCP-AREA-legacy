using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStamina))]
public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] float m_sneakSpeed;
    [SerializeField] float m_walkSpeed;
    [SerializeField] float m_runSpeed;

    public float GetPlayerSpeed()
    {
        float moveSpeed = m_walkSpeed;

        if (Input.GetButton("Sneak"))
        {
            moveSpeed = m_sneakSpeed;
            MainLinks.Instance.OnPlayerSneak.Invoke();
        }
        else if (Input.GetButton("Sprint") && MainLinks.Instance.PlayerStamina.HasPlayerЕnoughStaminaToRun)
        {
            MainLinks.Instance.OnPlayerRun.Invoke();
            return m_runSpeed;
        }

        if (Input.GetButtonUp("Sprint"))
        {
            MainLinks.Instance.OnPlayerStoppedRun.Invoke();
        }
        else if(Input.GetButtonUp("Sneak"))
        {
            MainLinks.Instance.OnPlayerStoppedSneak.Invoke();
        }

        return moveSpeed;
    }
}
