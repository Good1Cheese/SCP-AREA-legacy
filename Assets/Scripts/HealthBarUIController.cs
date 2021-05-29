using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarUIController : MonoBehaviour
{
    Slider healthBar;
    float cachedHealthValue;
     
    void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = MainLinks.Instance.PlayerHealthController.Health;
    }

    void Update()
    {
        float health = MainLinks.Instance.PlayerHealthController.Health;
        if (health != cachedHealthValue)
        {
            cachedHealthValue = health;
            UpdateUI(health);
        }
    }

    void UpdateUI(float value)
    {
        healthBar.value = value;
    }
}
