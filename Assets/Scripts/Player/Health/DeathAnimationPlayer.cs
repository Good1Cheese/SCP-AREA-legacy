using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

public class DeathAnimationPlayer : MonoBehaviour
{
    [SerializeField] private float _timeScaleOnDeath;
    [SerializeField] private float _delayAfterDeathAnimation;
    [SerializeField] private float _delayDuringBlackout;
    [SerializeField] private float _blackoutSpeed;

    [Inject] private readonly SceneTransition _sceneTransition;
    private Animator _playerDeathAnimator;
    private ColorAdjustments _colorAdjustments;
    private Vignette _vignette;
    private WaitForSeconds _timeoutDuringBlackout;
    private WaitForSeconds _timeoutAfterDeathAnimation;

    [Inject]
    private void Construct(Volume volume)
    {
        volume.profile.TryGet(out _colorAdjustments);
        volume.profile.TryGet(out _vignette);
    }

    private void Awake()
    {
        _playerDeathAnimator = GetComponent<Animator>();
        _timeoutAfterDeathAnimation = new WaitForSeconds(_delayAfterDeathAnimation);
        _timeoutDuringBlackout = new WaitForSeconds(_delayDuringBlackout);
    }

    public void PlayDeathAnimation()
    {
        Time.timeScale = _timeScaleOnDeath;
        _colorAdjustments.saturation.value = _colorAdjustments.saturation.min;

        _playerDeathAnimator.SetTrigger("OnPlayerDeath");
        StartCoroutine(PlayDeathAnimationCoroutine());
    }

    private IEnumerator PlayDeathAnimationCoroutine()
    {
        yield return _timeoutAfterDeathAnimation;

        do
        {
            _vignette.intensity.value += _blackoutSpeed;
            yield return _timeoutDuringBlackout;

            if (IsVingetteIntensityFull())
            {
                _colorAdjustments.colorFilter.value = Color.black;
            }
        }
        while (!IsVingetteIntensityFull());

        _sceneTransition.LoadScene((int)SceneTransition.Scenes.RespawnScene);
    }

    private bool IsVingetteIntensityFull()
    {
        return !(_vignette.intensity.value < _vignette.intensity.max);
    }
}
