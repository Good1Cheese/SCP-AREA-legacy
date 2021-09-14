using UnityEngine;
using Zenject;

public class SquatEffect : MonoBehaviour
{
    [SerializeField] float m_sneakHeadHeight;
    [Inject] readonly MovementSpeed m_playerSpeed;

    float m_startHeadHeight;

    void Start()
    {
        m_startHeadHeight = transform.localPosition.y;
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
        Vector3 localPosition = transform.localPosition;
        localPosition.y = newY;
        transform.localPosition = localPosition;
    }

    void OnDestroy()
    {
        m_playerSpeed.OnPlayerSneak -= LowerHeadHeight;
        m_playerSpeed.OnPlayerStoppedSneak -= RestoreHeadHeight;
    }
}
