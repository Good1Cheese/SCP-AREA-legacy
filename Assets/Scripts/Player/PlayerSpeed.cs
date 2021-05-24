using UnityEngine;

[RequireComponent(typeof(PlayerStamina))]
public class PlayerSpeed : MonoBehaviour
{
    [SerializeField] float sneakSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    PlayerStamina playerStamina;

    void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
    }

    public float GetPlayerSpeed()
    {
        float moveSpeed = walkSpeed;

        if (Input.GetButton("Sneak"))
        {
            moveSpeed = sneakSpeed;
        }
        else if (Input.GetButton("Sprint") && playerStamina.StaminaValue > 10)
        {
            moveSpeed = runSpeed;
            MainLinks.Instance.OnPlayerRunning.Invoke();
        }

        return moveSpeed;
    }
}
