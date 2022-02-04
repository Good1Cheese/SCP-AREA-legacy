using System.Collections;
using UnityEngine;
using Zenject;

public class WeaponRequestsHandler : RequestsHandler
{
    private AudioSource _audioSource;
    private bool _canInterrupt;

    private WeaponScriptBase _weaponScript;

    [Inject]
    private void Construct([Inject(Id = "Weapon")] AudioSource audioSource)
    {
        _audioSource = audioSource;
    }

    public void Handle(WeaponScriptBase weaponScriptBase)
    {
        if (CanNotHandle) { return; }

        _weaponScript = weaponScriptBase;

        if (_canInterrupt)
        {
            StartWithInterrupt();
            _canInterrupt = false;
            return;
        }

        StartWithoutInterrupt();
        _canInterrupt = _weaponScript.Interuppable;
    }

    protected override void StartCoroutine(IEnumerator enumerator)
    {
        _audioSource.PlayOneShot(_weaponScript.RequestClip);
        base.StartCoroutine(enumerator);
    }

    public override void StopCoroutine()
    {
        _audioSource.Stop();
        base.StopCoroutine();
    }

    protected override IEnumerator Coroutine()
    {
        print($"Weapon Action Started {_weaponScript}");

        _weaponScript.Interact();

        yield return _weaponScript.RequestTimeout;
        
        _weaponScript.OnSuccesRequest();
        IsCoroutineGoing = false;

        print($"Weapon Action Ended {_weaponScript}");
    }
}