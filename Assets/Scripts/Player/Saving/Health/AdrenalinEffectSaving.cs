using UnityEngine;
using Zenject;

public class AdrenalinEffectSaving : DataSaving
{
    [Inject] private readonly StaminaDisabler _staminaUseDisabler;

    public float effectTime;
    private bool _isEffectGoing;

    private void Start()
    {
        _staminaUseDisabler.Disabled += GetEffectTime;
    }

    private void GetEffectTime(float effectTime)
    {
        _isEffectGoing = true;
        this.effectTime = effectTime;
    }

    private void Update()
    {
        if (_isEffectGoing)
        {
            if (effectTime <= 0)
            {
                _isEffectGoing = false;
                return;
            }

            effectTime -= Time.deltaTime;
        }
    }

    public override void Save()
    {
        _isEffectGoing = false;
    }

    public override void Load()
    {
        if (effectTime > 0)
        {
            _staminaUseDisabler.Disable(effectTime);
            return;
        }
        _staminaUseDisabler.StopDisabling();
    }

    private void OnDestroy()
    {
        _staminaUseDisabler.Disabled -= GetEffectTime;
    }
}