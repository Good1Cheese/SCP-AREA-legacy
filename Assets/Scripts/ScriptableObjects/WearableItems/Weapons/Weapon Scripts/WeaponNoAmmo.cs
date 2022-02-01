public class WeaponNoAmmo : WeaponScriptBase, IInteractable
{
    public void ShootWithNoAmmo()
    {
        _weaponRequestsHandler.Handle(this, _weaponHandler.Weapon_SO.shotTimeout);
    }

    public void Interact()
    {
        
    }
}