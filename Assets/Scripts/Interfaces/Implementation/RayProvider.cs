using UnityEngine;

public class RayProvider : MonoBehaviour
{
    Transform _transform;

    void Start()
    {
        _transform = transform;
    }

    public Ray ProvideRay()
    {
        return new Ray(_transform.position, _transform.forward);
    }
}