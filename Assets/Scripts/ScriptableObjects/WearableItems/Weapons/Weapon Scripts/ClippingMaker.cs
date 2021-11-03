using UnityEngine;

public class ClippingMaker : MonoBehaviour
{
    [SerializeField] float m_clippingRayMaxDistance;
    [SerializeField] float m_riseForce;
    [SerializeField] float m_climpForce;
    bool m_isTriggerStaying;

    Transform ParentTransform => transform.parent.transform;

    public Transform PlayerTransform { get; set; }

    void Update()
    {
        if (!m_isTriggerStaying) { return; }

        Quaternion rotation = ParentTransform.rotation;
        rotation.x -= m_riseForce * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, -90, 0);

        ParentTransform.rotation = rotation;

        ParentTransform.Translate(m_climpForce * -PlayerTransform.forward * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (m_isTriggerStaying || other.transform == PlayerTransform) { return; }

        m_isTriggerStaying = true;
    }

    void OnTriggerExit(Collider other)
    {
        m_isTriggerStaying = false;
    }
}
