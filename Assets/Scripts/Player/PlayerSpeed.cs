using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStamina))]
public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] float _sneakSpeed;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    PlayerStamina playerStamina;

    void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
    }

    public float GetPlayerSpeed()
    {
        float moveSpeed = _walkSpeed;

        if (Input.GetButton("Sneak"))
        {
            moveSpeed = _sneakSpeed;
        }
        else if (Input.GetButton("Sprint") && playerStamina.StaminaValue > 10)
        {
            MainLinks.Instance.OnPlayerRunning.Invoke();
            return _runSpeed;
        }

        return moveSpeed;
    }
}
