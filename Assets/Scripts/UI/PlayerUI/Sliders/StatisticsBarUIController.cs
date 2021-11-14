using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public abstract class StatisticsBarUIController : MonoBehaviour
{
    [Inject] readonly GameLoader m_gameLoader;

    Slider m_slider;

    void Start()
    {
        m_slider = GetComponent<Slider>();
        float startBarValue = GetValue();
        m_slider.maxValue = startBarValue;
        m_slider.value = startBarValue;

        m_gameLoader.OnGameLoadingUI += gameObject.SetActive;
        Subscribe();
    }

    protected void UpdateUI()
    {
        m_slider.value = GetValue();
    }

    protected abstract float GetValue();
    protected abstract void Subscribe();
    protected abstract void Unsubscribe();

    void OnDestroy()
    {
        m_gameLoader.OnGameLoadingUI -= gameObject.SetActive;
        Unsubscribe();
    }

}