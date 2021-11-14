using UnityEngine;
using Zenject;

public class SlowWalkEffect : MonoBehaviour
{
    [SerializeField] float m_yChangeTime;
    [SerializeField] AnimationCurve m_slowWalkYChangeCurve;
    [SerializeField] CharacterController m_characterController;

    [Inject] readonly SlowWalkController m_slowWalkController;

    float m_slowWalkTime;

    void Start()
    {
        m_slowWalkController.OnPlayerStartedUseOfMove += SetSlowWalkTimeToZero;
        m_slowWalkController.OnPlayerStoppedUseOfMove += SetSlowWalkTimeToMaxValue;
        m_slowWalkController.OnPlayerUsingMove += ActivateEffect;
        m_slowWalkController.OnPlayerNotUsingMove += DeactivateEffect;
    }

    void SetSlowWalkTimeToZero()
    {
        m_slowWalkTime = 0;
    }

    void SetSlowWalkTimeToMaxValue()
    {
        m_slowWalkTime = m_yChangeTime;
    }

    void ActivateEffect()
    {
        m_slowWalkTime += Time.deltaTime;
        m_characterController.height = m_slowWalkYChangeCurve.Evaluate(m_slowWalkTime);
    }

    void DeactivateEffect()
    {
        m_slowWalkTime -= Time.deltaTime;
        m_characterController.height = m_slowWalkYChangeCurve.Evaluate(m_slowWalkTime);
    }

    void OnDestroy()
    {
        m_slowWalkController.OnPlayerStartedUseOfMove -= SetSlowWalkTimeToZero;
        m_slowWalkController.OnPlayerStoppedUseOfMove -= SetSlowWalkTimeToMaxValue;
        m_slowWalkController.OnPlayerUsingMove -= ActivateEffect;
        m_slowWalkController.OnPlayerNotUsingMove -= DeactivateEffect;
    }
}
