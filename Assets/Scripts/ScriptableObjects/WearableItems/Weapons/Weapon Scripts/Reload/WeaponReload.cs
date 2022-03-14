using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

public class WeaponReload : WeaponUser
{
    [SerializeField] private ReloadStage[] _reloadStages;

    private AmmoPackage _ammoPackage;
    private WeaponRequestsHandler _weaponRequestsHandler;

    public ItemSlot<AmmoHandler> NextClipAmmo => _ammoPackage.Clips.Slots
            .Where(slot => slot.HasItem)
            .OrderByDescending(slot => slot.Item.Ammo)
            .FirstOrDefault();

    public bool HasAmmo => _ammoPackage.Clips.Slots
                .TakeWhile(slot => slot.Item != null)
                .Sum(slot => slot.Item.Ammo) > 0;

    [Inject]
    private void Inject(AmmoPackage ammoPackage, WeaponRequestsHandler weaponRequestsHandler)
    {
        _ammoPackage = ammoPackage;
        _weaponRequestsHandler = weaponRequestsHandler;
    }

    public void Reload()
    {
        _weaponHandler.CurrentClipSlot.Item = null;
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        ReloadStage nextStage;

        while ((nextStage = Array.Find(_reloadStages, stage => !stage.Done)) != null)
        {
            _weaponRequestsHandler.Handle(nextStage);

            yield return nextStage.InteractionTimeout;

            nextStage.Done = true;

            if (!(_weaponRequestsHandler.WeaponScript is ReloadStage)) { yield break; }
        }

        ResetAllStages();
    }

    private void ResetAllStages()
    {
        for (int i = 0; i < _reloadStages.Length; i++)
        {
            _reloadStages[i].Done = false;
        }
    }
}