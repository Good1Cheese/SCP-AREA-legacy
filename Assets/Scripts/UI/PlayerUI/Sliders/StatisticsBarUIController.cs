using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public abstract class StatisticsBarUIController : MonoBehaviour
{
    [Inject] private readonly GameLoader _gameLoader;
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        float startBarValue = GetValue();
        _slider.maxValue = startBarValue;
        _slider.value = startBarValue;

        _gameLoader.OnGameLoadingUI += gameObject.SetActive;
        Subscribe();
    }

    public void UpdateUI()
    {
        _slider.value = GetValue();
    }

    protected abstract float GetValue();
    protected abstract void Subscribe();
    protected abstract void Unsubscribe();

    private void OnDestroy()
    {
        _gameLoader.OnGameLoadingUI -= gameObject.SetActive;
        Unsubscribe();
    }

}