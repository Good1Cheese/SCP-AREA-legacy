using UnityEngine;
using Zenject;

public class BleedingDataSaving : DataHandler
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public float bleedTime;
    bool m_isBleedingGoing;

    void Awake()
    {
        m_characterBleeding.OnPlayerBleedingStarted += GetBleedingTime;
        m_characterBleeding.OnPlayerBleedingEnded += SaveData;
    }

    void Update()
    {
        if (m_isBleedingGoing)
        {
            if (bleedTime <= 0) { m_isBleedingGoing = false; }
            bleedTime -= Time.deltaTime;
        }
    }

    void GetBleedingTime()
    {
        m_isBleedingGoing = true;
        bleedTime = m_characterBleeding.DelayDuringBleeding;
    }

    public override void SaveData()
    {
        m_isBleedingGoing = false;
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