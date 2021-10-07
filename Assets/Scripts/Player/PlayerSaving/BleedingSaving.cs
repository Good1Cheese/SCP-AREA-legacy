using UnityEngine;
using Zenject;

public class BleedingSaving : DataSaving
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public float bleedTime;
    bool m_wasDataSaved;
    bool m_isBleedingGoing;

    void Awake()
    {
        m_characterBleeding.OnPlayerBleedingStarted += GetBleedingTime;
        m_characterBleeding.OnPlayerBleeding += GetBleedingTime;
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
        if (m_wasDataSaved) { return; }
        m_isBleedingGoing = true;
        bleedTime = m_characterBleeding.DelayDuringBleeding;
    }

    public override void Save()
    {
        m_wasDataSaved = true;
        m_isBleedingGoing = false;
    }

    public override void LoadData()
    {
        if (bleedTime > 0) 
        {
            m_characterBleeding.StopBleeding();
            m_characterBleeding.CreateBleedingTimeout(bleedTime);
            m_characterBleeding.Bleed();
            return;
        }
        m_characterBleeding.StopBleedingWithoutNotify();
    }

    void OnDestroy()
    {
        m_characterBleeding.OnPlayerBleedingStarted -= GetBleedingTime;
        m_characterBleeding.OnPlayerBleeding -= GetBleedingTime;
        m_characterBleeding.OnPlayerBleedingEnded -= Save;
    }
}