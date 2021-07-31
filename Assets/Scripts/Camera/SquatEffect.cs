using UnityEngine;
using Zenject;

public class SquatEffect : MonoBehaviour
{
    [SerializeField] float m_sneakHeadHeight;
    [Inject] readonly MovementSpeed m_playerSpeed;

    float m_startHeadHeight;
    Transform m_transform;

    void Start()
    {
        m_transform = transform;
        m_startHeadHeight = m_transform.localPosition.y;
        m_playerSpeed.OnPlayerSneak += LowerHeadHeight;
        m_playerSpeed.OnPlayerStoppedSneak += RestoreHeadHeight;
    }

    void LowerHeadHeight()
    {
        SetYByLocalPosition(m_sneakHeadHeight);
    }

    void RestoreHeadHeight()
    {
        SetYByLocalPosition(m_startHeadHeight);
    }

    void SetYByLocalPosition(float newY)
    {
        Vector3 localPosition = m_transform.localPosition;
        localPosition.y = newY;
        m_transform.localPosition = localPosition;
    }

    void OnDestroy()
    {
        m_playerSpeed.OnPlayerSneak -= LowerHeadHeight;
        m_playerSpeed.OnPlayerStoppedSneak -= RestoreHeadHeight;
    }
}
