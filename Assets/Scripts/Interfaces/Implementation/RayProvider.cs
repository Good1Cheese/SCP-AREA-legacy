using UnityEngine;

public class RayProvider : MonoBehaviour
{
    Transform m_transform;
    Ray m_ray;

    void Start()
    {
        m_transform = transform;
        m_ray = new Ray();
    }

    public Ray ProvideRay()
    {
        m_ray.origin = m_transform.position;
        m_ray.direction = m_transform.forward;
        return m_ray;
    }

}