using UnityEngine;

public class RayProvider : MonoBehaviour
{
    public Ray ProvideRay()
    {
        return new Ray(transform.position, transform.forward);
    }
}