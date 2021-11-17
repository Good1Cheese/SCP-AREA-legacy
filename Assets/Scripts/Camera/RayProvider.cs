using UnityEngine;

public class RayProvider : MonoBehaviour, IRayProvider
{
    [SerializeField] private Transform _transform;
    private Ray _ray;

    private void Start()
    {
        _ray = new Ray();
    }

    public Ray ProvideRay()
    {
        _ray.origin = _transform.position;
        _ray.direction = _transform.forward;
        return _ray;
    }

    private void Update()
    {
        ProvideRay();
    }
}