using System.Collections;
using Zenject;

public class WeaponReloadCoroutineUser : CoroutineUser
{
    [Inject] private readonly WeaponReload _weaponReload;
    [Inject] private readonly WeaponSlot _weaponSlot;

    private void Awake()
    {
        _weaponSlot.OnNewActionStarted += StopReload;
    }

    private void StopReload()
    {
        StopAction();
    }

    protected override IEnumerator Action => _weaponReload.Reload();
    protected override IEnumerator Coroutine() { yield break; }

    private void OnDestroy()
    {
        _weaponSlot.OnNewActionStarted -= StopReload;
    }
}