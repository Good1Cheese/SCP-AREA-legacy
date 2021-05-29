using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StaminaBarUIController : MonoBehaviour
{
    Slider staminaBar;
    float cachedStaminaValue;

    void Start()
    {
        staminaBar = GetComponent<Slider>();
        staminaBar.maxValue = MainLinks.Instance.PlayerStamina.StaminaValue;
    }

    void Update()
    {
        float staminaValue = MainLinks.Instance.PlayerStamina.StaminaValue;
        if (staminaValue != cachedStaminaValue)
        {
            cachedStaminaValue = staminaValue;
            UpdateUI(staminaValue);
        }
    }

    void UpdateUI(float value)
    {
        staminaBar.value = value;
    }
}


//[RequireComponent(typeof(Slider))]
//public abstract class StatisticsBarUIController : MonoBehaviour
//{
//    Slider slider;
//    float cachedSliderValue;

//    public abstract float GetSecretValue();

//    void Start()
//    {
//        slider = GetComponent<Slider>();
//        slider.maxValue = GetSecretValue();
//    }

//    void Update()
//    {
//        float staminaValue = GetSecretValue();
//        if (staminaValue != cachedSliderValue)
//        {
//            cachedSliderValue = staminaValue;
//            UpdateUI(staminaValue);
//        }
//    }

//    void UpdateUI(float value)
//    {
//        slider.value = value;
//    }
//}

//public class StaminaBarUIController : StatisticsBarUIController
//{
//    public override float GetSecretValue()
//    {
//        return MainLinks.Instance.PlayerStamina.StaminaValue;
//    }
//}

//public class HealthBarUIController : StatisticsBarUIController
//{
//    public override float GetSecretValue()
//    {
//        return MainLinks.Instance.PlayerHealthController.Health;
//    }
//}

