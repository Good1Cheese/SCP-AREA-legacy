using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    [SerializeField] GameObject flashLight;
    void Update()
    {
        if (Input.GetButtonDown("FlashLight"))
        {
            flashLight.SetActive(!flashLight.activeSelf);
        }
    }
}
