﻿using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public abstract class StatisticsBarUIController : MonoBehaviour
{
    protected GameLoader _gameLoader;
    private Slider _slider;

    public Slider Slider { get => _slider; }

    [Inject]
    private void Construct(GameLoader gameLoader)
    {
        _gameLoader = gameLoader;
    }

    protected void Start()
    {
        _slider = GetComponent<Slider>();
        float startBarValue = GetValue();

        _slider.maxValue = startBarValue;
        _slider.value = startBarValue;

        _gameLoader.UILoading += gameObject.SetActive;
        Subscribe();
    }

    public virtual void UpdateUI()
    {
        _slider.value = GetValue();
    }

    protected abstract float GetValue();
    protected abstract void Subscribe();
    protected abstract void Unsubscribe();

    private void OnDestroy()
    {
        _gameLoader.UILoading -= gameObject.SetActive;
        Unsubscribe();
    }
}