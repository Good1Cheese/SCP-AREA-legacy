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
        Vector3 localPosition = m_transform.localPosition;
        localPosition.y = m_sneakHeadHeight;
        m_transform.localPosition = localPosition;
    }

    void RestoreHeadHeight()
    {
        Vector3 localPosition = m_transform.localPosition;
        localPosition.y = m_startHeadHeight;
        m_transform.localPosition = localPosition;
    }

    void OnDestroy()
    {
        m_playerSpeed.OnPlayerSneak -= LowerHeadHeight;
        m_playerSpeed.OnPlayerStoppedSneak -= RestoreHeadHeight;
    }
}
