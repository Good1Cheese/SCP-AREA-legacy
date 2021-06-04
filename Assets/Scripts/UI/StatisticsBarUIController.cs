using UnityEngine;
using UnityEngine.UI;
//[RequireComponent(typeof(Slider))]
//public class StaminaBarUIController : MonoBehaviour
//{
//    Slider staminaBar;
//    float cachedStaminaValue;

//    void Start()
//    {
//        staminaBar = GetComponent<Slider>();
//        staminaBar.maxValue = MainLinks.Instance.PlayerStamina.StaminaValue;
//    }

//    void Update()
//    {
//        float staminaValue = MainLinks.Instance.PlayerStamina.StaminaValue;
//        if (staminaValue != cachedStaminaValue)
//        {
//            cachedStaminaValue = staminaValue;
//            UpdateUI(staminaValue);
//        }
//    }

//    void UpdateUI(float value)
//    {
//        staminaBar.value = value;
//    }
//}


[RequireComponent(typeof(Slider))]
public abstract class StatisticsBarUIController : MonoBehaviour
{
    Slider _sliderBar;
    float _cachedBarValue;

    public abstract float GetBarValue();

    void Start()
    {
        _sliderBar = GetComponent<Slider>();
        _sliderBar.maxValue = GetBarValue();
    }

    void Update()
    {
        float barValue = GetBarValue();
        if (barValue != _cachedBarValue)
        {
            _cachedBarValue = barValue;
            UpdateUI(barValue);
        }
    }

    void UpdateUI(float value)
    {
        _sliderBar.value = value;
    }
}
