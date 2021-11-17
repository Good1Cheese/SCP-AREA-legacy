using UnityEngine;

public class RotationResetter : MonoBehaviour
{
    [SerializeField] private float _smoothTime;
    private bool _exitedFromTrigger;

    private void Update()
    {
        if (_exitedFromTrigger)
        {
            if (transform.localRotation == Quaternion.identity)
            {
                _exitedFromTrigger = false;
                return;
            }

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, _smoothTime * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        _exitedFromTrigger = true;
        transform.localPosition = Vector3.zero;
    }
}
