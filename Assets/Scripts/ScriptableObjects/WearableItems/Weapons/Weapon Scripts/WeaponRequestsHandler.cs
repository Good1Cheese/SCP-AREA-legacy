using System.Collections;
using UnityEngine;
using Zenject;

public class WeaponRequestsHandler : CoroutineUser
{
    private AudioSource _audioSource;
    private PickableInventoryToggler _pickableInventoryToggler;
    private AmmoUIEnablerDisabler _ammoUIEnablerDisabler;
    private PauseMenuToggler _pauseMenuToggler;

    public bool IsCurrentInterruptable { get; set; }
    public WeaponScriptBase WeaponScript { get; set; }

    [Inject]
    private void Construct([Inject(Id = "Weapon")] AudioSource audioSource,
                           PickableInventoryToggler pickableInventoryToggler,
                           AmmoUIEnablerDisabler ammoUIEnablerDisabler,
                           PauseMenuToggler pauseMenuToggler)
    {
        _audioSource = audioSource;
        _pickableInventoryToggler = pickableInventoryToggler;
        _ammoUIEnablerDisabler = ammoUIEnablerDisabler;
        _pauseMenuToggler = pauseMenuToggler;
    }

    public void Handle(WeaponScriptBase weaponScriptBase)
    {
        if (_pickableInventoryToggler.IsToggled
            || _pauseMenuToggler.IsToggled
            || _ammoUIEnablerDisabler.IsActivated) { return; }

        WeaponScript = weaponScriptBase;
        StartWithoutInterrupt();
    }

    protected override void StartCoroutine(IEnumerator enumerator)
    {
        base.StartCoroutine(enumerator);

        if (WeaponScript.Sound == null) { return; }

        _audioSource.PlayOneShot(WeaponScript.Sound);
    }

    public override void StopCoroutine()
    {
        base.StopCoroutine();
        _audioSource.Stop();
    }

    protected override IEnumerator Coroutine()
    {
        WeaponScript.Interact();

        yield return WeaponScript.InteractionTimeout;

        IsCoroutineGoing = false;
    }
}