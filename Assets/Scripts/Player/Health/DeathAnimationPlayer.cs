using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

public class DeathAnimationPlayer : MonoBehaviour
{
    [SerializeField] float m_timeScaleOnDeath;
    [SerializeField] float m_delayAfterDeathAnimation;
    [SerializeField] float m_delayDuringBlackout;
    [SerializeField] float m_blackoutSpeed;

    [Inject] readonly SceneTransition m_sceneTransition;

    Animator m_playerDeathAnimator;
    ColorAdjustments m_colorAdjustments;
    Vignette m_vignette;

    WaitForSeconds m_timeoutDuringBlackout;
    WaitForSeconds m_timeoutAfterDeathAnimation;

    [Inject]
    void Construct(Volume volume)
    {
        volume.profile.TryGet(out m_colorAdjustments);
        volume.profile.TryGet(out m_vignette);
    }

    void Awake()
    {
        m_playerDeathAnimator = GetComponent<Animator>();
        m_timeoutAfterDeathAnimation = new WaitForSeconds(m_delayAfterDeathAnimation);
        m_timeoutDuringBlackout = new WaitForSeconds(m_delayDuringBlackout);
    }

    public void PlayDeathAnimation()
    {
        Time.timeScale = m_timeScaleOnDeath;
        m_colorAdjustments.saturation.value = m_colorAdjustments.saturation.min;

        m_playerDeathAnimator.SetTrigger("OnPlayerDeath");
        StartCoroutine(PlayDeathAnimationCoroutine());
    }

    IEnumerator PlayDeathAnimationCoroutine()
    {
        yield return m_timeoutAfterDeathAnimation;

        do
        {
            m_vignette.intensity.value += m_blackoutSpeed;
            yield return m_timeoutDuringBlackout;

            if (IsVingetteIntensityFull())
            {
                m_colorAdjustments.colorFilter.value = Color.black;
            }
        }
        while (!IsVingetteIntensityFull());

        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.RespawnScene);
    }

    bool IsVingetteIntensityFull()
    {
        return !(m_vignette.intensity.value < m_vignette.intensity.max);
    }
}
