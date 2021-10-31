using UnityEngine;

public class ClippingMaker : MonoBehaviour
{
    [SerializeField] float m_clippingRayMaxDistance;
    [SerializeField] float m_riseForce;
    [SerializeField] float m_climpForce;

    Transform ParentTransform => transform.parent.transform;

    public Transform PlayerTransform { get; set; }

    void OnTriggerStay(Collider other)
    {
        if (!Physics.Raycast(transform.position, transform.forward, m_clippingRayMaxDistance))
        {
            Quaternion rotation = ParentTransform.rotation;
            rotation.x -= m_riseForce * Time.deltaTime;
            rotation.x = Mathf.Clamp(rotation.x, -90, 0);

            ParentTransform.rotation = rotation;

            ParentTransform.Translate(m_climpForce * -PlayerTransform.forward * Time.deltaTime);
        }
    }
}
