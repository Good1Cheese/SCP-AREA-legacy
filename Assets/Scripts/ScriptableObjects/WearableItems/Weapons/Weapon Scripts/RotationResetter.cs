using UnityEngine;

public class RotationResetter : MonoBehaviour
{
    [SerializeField] float m_smoothTime;

    bool m_exitedFromTrigger;

    void Update()
    {
        if (m_exitedFromTrigger)
        {
            if (transform.localRotation == Quaternion.identity)
            {
                m_exitedFromTrigger = false;
                return;
            }

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, m_smoothTime * Time.deltaTime);
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        m_exitedFromTrigger = true;
        transform.localPosition = Vector3.zero;
    }
}
