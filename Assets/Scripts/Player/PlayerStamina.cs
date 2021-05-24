using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] Slider staminaBar;
    [SerializeField] float staminaRegenerationSpeed;
    [SerializeField] float staminaBurningSpeed;
    [SerializeField] float staminaRegenerationDecoy;
    public float StaminaValue { get => staminaBar.value; }

    void Start()
    {
        MainLinks.Instance.OnPlayerRunning += BurnStamina;
    }

    IEnumerator RegenerateStamina(float staminaValue)
    {
        yield return new WaitForSeconds(staminaRegenerationDecoy);

        if (staminaBar.value == staminaValue)
        {
            float previousStaminaValue = 0;
            while (staminaBar.value < staminaBar.maxValue && !(previousStaminaValue > staminaBar.value))
            {
                staminaBar.value += staminaRegenerationSpeed;
                previousStaminaValue = staminaBar.value;
                yield return new WaitForSeconds(0.05f);
            }

        }
    }

    void BurnStamina()
    {
        staminaBar.value -= staminaBurningSpeed;
        StartCoroutine(RegenerateStamina(staminaBar.value));
    }
}


