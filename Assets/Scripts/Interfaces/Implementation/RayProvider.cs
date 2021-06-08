using UnityEngine;

public class RayProvider : MonoBehaviour
{
    Transform m_transform;

    void Start()
    {
        m_transform = transform;
    }

    public Ray ProvideRay()
    {
        return new Ray(m_transform.position, m_transform.forward);
    }
}