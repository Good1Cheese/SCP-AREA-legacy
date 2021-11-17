using UnityEngine;

public class ClippingMaker : MonoBehaviour
{
    [SerializeField] private float _clippingRayMaxDistance;
    [SerializeField] private float _riseForce;
    [SerializeField] private float _climpForce;
    private bool _isTriggerStaying;

    private Transform ParentTransform => transform.parent.transform;

    public Transform PlayerTransform { get; set; }

    private void Update()
    {
        if (!_isTriggerStaying) { return; }

        Quaternion rotation = ParentTransform.rotation;
        rotation.x -= _riseForce * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, -90, 0);

        ParentTransform.rotation = rotation;

        ParentTransform.Translate(_climpForce * -PlayerTransform.forward * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isTriggerStaying || other.transform == PlayerTransform) { return; }

        _isTriggerStaying = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isTriggerStaying = false;
    }
}
