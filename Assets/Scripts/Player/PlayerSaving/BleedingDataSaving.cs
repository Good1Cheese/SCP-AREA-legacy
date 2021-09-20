using UnityEngine;
using Zenject;

public class BleedingDataSaving : DataSaving
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public float bleedTime;
    bool m_isBleedingGoing;

    void Awake()
    {
        m_characterBleeding.OnPlayerBleedingStarted += GetBleedingTime;
        m_characterBleeding.OnPlayerBleedingEnded += Save;
    }

    void Update()
    {
        if (m_isBleedingGoing)
        {
            if (bleedTime <= 0) 
            { 
                m_isBleedingGoing = false;
                return; 
            }

            bleedTime -= Time.deltaTime;
        }
    }

    void GetBleedingTime()
    {
        m_isBleedingGoing = true;
        bleedTime = m_characterBleeding.DelayDuringBleeding;
    }

    public override void Save()
    {
        m_isBleedingGoing = false;
    }

    public override void Load()
    {
        if (bleedTime > 0) 
        {
            m_characterBleeding.CreateBleedingTimeout(bleedTime);
            m_characterBleeding.Bleed();
            return;
        }
        m_characterBleeding.StopBleeding();
    }

    void OnDestroy()
    {
        m_characterBleeding.OnPlayerBleedingStarted -= GetBleedingTime;
        m_characterBleeding.OnPlayerBleedingEnded -= Save;
    }
}