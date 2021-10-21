using UnityEngine;
using Zenject;

public class SentryRotator : MonoBehaviour
{
    [SerializeField] Transform m_sentryGun;
    [SerializeField] float m_smoothTime;

    [Inject] readonly GameObject m_playerGameobject;

    bool m_isPlayerComedInTrigger;

    void OnTriggerEnter(Collider other)
    {
        m_isPlayerComedInTrigger = other.gameObject == m_playerGameobject;
    }

    void OnTriggerStay(Collider other)
    {
        if (!m_isPlayerComedInTrigger) { return; }

        Rotate();
    }


    void Rotate()
    {
        Vector3 relativePos = m_sentryGun.position - m_playerGameobject.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        m_sentryGun.rotation = Quaternion.Slerp(m_sentryGun.rotation, targetRotation, m_smoothTime * Time.deltaTime);
    }

}