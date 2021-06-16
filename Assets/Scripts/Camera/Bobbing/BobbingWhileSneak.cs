//using UnityEngine;

//class BobbingWhileSneak : MonoBehaviour
//{
//    [SerializeField] float m_bobFrequencyWhileSneak;
//    [SerializeField] float m_bobVerticalAmplitudeWhileSneak;

//    CameraBobbing m_cameraBobbing;

//    void Awake()
//    {
//        m_cameraBobbing = GetComponent<CameraBobbing>();
//    }

//    void Start()
//    {
//        MainLinks.Instance.PlayerSpeed.OnPlayerSneak += ChangeBobbingDuringRun;
//        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedSneak += m_cameraBobbing.ResetBobbingValues;
//    }

//    void ChangeBobbingDuringRun()
//    {
//        m_cameraBobbing.BobFrequency = m_bobFrequencyWhileSneak;
//        m_cameraBobbing.BobVerticalAmplitude = m_bobVerticalAmplitudeWhileSneak;
//    }


//    void OnDisable()
//    {
//        MainLinks.Instance.PlayerSpeed.OnPlayerSneak -= ChangeBobbingDuringRun;
//        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedSneak -= m_cameraBobbing.ResetBobbingValues;
//    }
//}

class BobbingWhileSneak : BobbingChangeWhileAction
{
    protected override void Subscribe()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerSneak += ChangeBobbingDuringRun;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedSneak += m_cameraBobbing.ResetBobbingValues;
    }

    protected override void Unsubscribe()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerSneak -= ChangeBobbingDuringRun;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedSneak -= m_cameraBobbing.ResetBobbingValues;
    }
}

