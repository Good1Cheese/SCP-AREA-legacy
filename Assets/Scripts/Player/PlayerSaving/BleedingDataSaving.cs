using UnityEngine;
using Zenject;

public class BleedingDataSaving : DataHandler
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public float bleedTime;
    bool IsBleedingGoing;

    void Awake()
    {
        m_characterBleeding.OnPlayerBleedingStarted += GetBleedingTime;
        m_characterBleeding.OnPlayerBleedingEnded += SaveData;
    }

    void Update()
    {
        if (IsBleedingGoing)
        {
            if (bleedTime <= 0) { IsBleedingGoing = false; }
            bleedTime -= Time.deltaTime;
        }
    }

    void GetBleedingTime()
    {
        IsBleedingGoing = true;
        bleedTime = m_characterBleeding.DelayDuringBleeding;
    }

    public override void SaveData()
    {
        IsBleedingGoing = false;
    }

    public override void LoadData()
    {
        if (bleedTime > 0) 
        {
            m_characterBleeding.CreateBleedingTimeout(bleedTime);
            return;
        }
        m_characterBleeding.StopBleeding();
    }

    void OnDestroy()
    {
        m_characterBleeding.OnPlayerBleedingStarted -= GetBleedingTime;
        m_characterBleeding.OnPlayerBleedingEnded -= SaveData;
    }
}