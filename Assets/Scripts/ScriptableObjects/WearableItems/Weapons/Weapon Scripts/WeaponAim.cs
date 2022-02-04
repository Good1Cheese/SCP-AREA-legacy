using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(IRayProvider))]
public class WeaponAim : WeaponScriptBase
{
    private const KeyCode AIM_KEY = KeyCode.Mouse1;

    [SerializeField] private float _aimDelay;

    private WaitForSeconds _aimTimeout;
    private Animator _weaponAnimator;
    private bool _isAiming;

    public bool IsAiming { get; set; }

    public Action FiredWithAim { get; set; }
    public Action FiredWithoutAim { get; set; }

    public Action Aimed { get; set; }
    public Action Unaimed { get; set; }
    public bool WasAimed { get; set; }

    public override WaitForSeconds RequestTimeout => _aimTimeout;
    public override AudioClip RequestClip => null;


    [Inject]
    private void Inject(Animator weaponAnimator)
    {
        _weaponAnimator = weaponAnimator;
    }

    private void Awake()
    {
        _aimTimeout = new WaitForSeconds(_aimDelay);
    }

    private void Update()
    {
        if (Input.GetKeyDown(AIM_KEY))
        {
            SetAimStateWithTriggerCheck(true);
        }

        if (Input.GetKeyUp(AIM_KEY))
        {
            if (!_weaponAnimator.GetBool("Aimed")) { return; }

            SetAimState(false);
        }
    }

    public void SetAimStateWithTriggerCheck(bool isAiming)
    {
        if (_weaponHandler == null) { return; }

        WasAimed = _weaponHandler.ClippingMaker.GameObjectTrigger.IsTriggered;

        if (WasAimed) { return; }

        SetAimState(isAiming);
    }

    public void SetAimState(bool isAiming)
    {
        _isAiming = isAiming;
        _weaponRequestsHandler.Handle(this);
    }

    private void Aim()
    {
        if (CanNotWeaponDoAction()) { return; }

        IsAiming = _isAiming;
        _weaponAnimator.SetBool("Aimed", _isAiming);

        if (IsAiming)
        {
            Aimed?.Invoke();
            return;
        }
        Unaimed?.Invoke();
    }

    protected override void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        base.SetWeaponHandler(weaponHandler);
        _weaponAnimator.runtimeAnimatorController = weaponHandler.Weapon_SO.weaponAnimationContoller;
    }

    public override void Interact() => Aim();
}