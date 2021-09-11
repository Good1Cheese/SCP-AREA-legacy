﻿using UnityEngine;

[CreateAssetMenu(fileName = "new Bandage", menuName = "ScriptableObjects/Bandage")]
public class Bandage_SO : PickableItem_SO
{
    CharacterBleeding m_playerBleeding;

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        base.GetDependencies(playerInstaller);
        m_playerBleeding = playerInstaller.CharacterBleeding;
    }

    public override void Use()
    {
        m_playerBleeding.StopBleeding();
    }
}