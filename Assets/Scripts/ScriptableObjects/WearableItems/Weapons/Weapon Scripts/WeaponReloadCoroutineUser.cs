using System.Collections;
using Zenject;

public class WeaponReloadCoroutineUser : CoroutineUser
{
    [Inject] private readonly WeaponReload _weaponReload;

    protected override IEnumerator Action => _weaponReload.Reload();
    protected override IEnumerator Coroutine() { yield break; }
}