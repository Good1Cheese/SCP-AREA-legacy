using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] float _regenerationDelay;
    [SerializeField] float _regenerationSpeed;
    [SerializeField] float _spendingSpeed;
    [SerializeField] float _maxStaminaAmount;
    WaitForSeconds waitForSeconds;
    WaitForSeconds waitForSeconds2;
    public float StaminaValue { get; set; }

    void Awake()
    {
        StaminaValue = _maxStaminaAmount;
        MainLinks.Instance.PlayerStamina = this;
        MainLinks.Instance.OnPlayerRunning += BurnStamina;
    }

    void Start()
    {
        waitForSeconds = new WaitForSeconds(_regenerationDelay);
        waitForSeconds2 = new WaitForSeconds(0.05f);
    }

    IEnumerator RegenerateStaminaCoroutine(float staminaAmount)
    {
        yield return waitForSeconds;

        bool wasPlayerRunningAfterTimeOut = StaminaValue != staminaAmount;
        if (!wasPlayerRunningAfterTimeOut)
        {
            float previousStaminaValue = 0;
            while (StaminaValue < _maxStaminaAmount && !(previousStaminaValue > StaminaValue))
            {
                StaminaValue += _regenerationSpeed;
                previousStaminaValue = StaminaValue;
                yield return waitForSeconds2;
            }
        }
    }

    public void BurnStamina()
    {
        StaminaValue -= _spendingSpeed;
        StartCoroutine(RegenerateStaminaCoroutine(StaminaValue));
    }
}


