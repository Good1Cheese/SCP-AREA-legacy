using UnityEngine;
using Zenject;

public class SneakEffect : MonoBehaviour
{
    [SerializeField] AnimationCurve m_slowWalkYChange;
    [SerializeField] CharacterController m_characterController;

    [Inject] readonly SlowWalkController m_slowWalkController;

    void Start()
    {
        m_slowWalkController.OnPlayerUsingMove += ActivateEffect;
        m_slowWalkController.OnPlayerNotUsingMove += DeactivateEffect;
    }

    void ActivateEffect()
    {
        if (m_slowWalkController.MoveTime >= 1) { return; }

        m_characterController.height = m_slowWalkYChange.Evaluate(m_slowWalkController.MoveTime);
    }

    void DeactivateEffect()
    {
        if (m_slowWalkController.MoveTime <= 0) { return; }

        m_characterController.height = m_slowWalkYChange.Evaluate(m_slowWalkController.MoveTime);
    }

    void OnDestroy()
    {
        m_slowWalkController.OnPlayerStartedUseOfMove -= ActivateEffect;
        m_slowWalkController.OnPlayerStoppedUseOfMove -= DeactivateEffect;
    }
}
