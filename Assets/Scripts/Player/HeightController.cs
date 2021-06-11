using UnityEngine;

public class HeightController : MonoBehaviour
{
    [SerializeField] float m_sneakHeadHeight;

    float m_startHeadHeight;
    Transform m_transform;

    void Start()
    {
        m_transform = transform;
        m_startHeadHeight = m_transform.localPosition.y;
        MainLinks.Instance.PlayerSpeed.OnPlayerSneak += LowerHeadHeight;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedSneak += RestoreHeadHeight;
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

    void OnDisable()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerSneak -= LowerHeadHeight;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedSneak -= RestoreHeadHeight;
    }
}
