using UnityEngine;
using Zenject;

public class SentryRotator : MonoBehaviour
{
    [SerializeField] private Transform _sentryGun;
    [SerializeField] private float _smoothTime;

    [Inject] private readonly GameObject _playerGameobject;
    private bool _isPlayerComedInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        _isPlayerComedInTrigger = other.gameObject == _playerGameobject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isPlayerComedInTrigger) { return; }

        Rotate();
    }

    private void Rotate()
    {
        Vector3 relativePos = _sentryGun.position - _playerGameobject.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

        _sentryGun.rotation = Quaternion.Slerp(_sentryGun.rotation, targetRotation, _smoothTime * Time.deltaTime);
    }

}