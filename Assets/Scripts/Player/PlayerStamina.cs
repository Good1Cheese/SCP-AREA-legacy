using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] float staminaRegenerationSpeed;
    [SerializeField] float staminaBurningSpeed;
    [SerializeField] float staminaRegenerationDecoy;

    [SerializeField] float maxStaminaAmout;
    public float StaminaValue { get; set; }

    void Awake()
    {
        StaminaValue = maxStaminaAmout;
        MainLinks.Instance.PlayerStamina = this;
        MainLinks.Instance.OnPlayerRunning += BurnStamina;
    }

    IEnumerator RegenerateStamina(float currentStaminaAmout)
    {
        yield return new WaitForSeconds(staminaRegenerationDecoy);

        if (StaminaValue == currentStaminaAmout)
        {
            float previousStaminaValue = 0;
            while (StaminaValue < maxStaminaAmout && !(previousStaminaValue > StaminaValue))
            {
                StaminaValue += staminaRegenerationSpeed;
                previousStaminaValue = StaminaValue;
                yield return new WaitForSeconds(0.05f);
            }

        }
    }

    void BurnStamina()
    {
        StaminaValue -= staminaBurningSpeed;
        StartCoroutine(RegenerateStamina(StaminaValue));
    }
}


