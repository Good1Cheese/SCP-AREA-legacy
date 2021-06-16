//using UnityEngine;

//class BobbingWhileRun : MonoBehaviour
//{
//    [SerializeField] float m_bobFrequencyWhileRun;
//    [SerializeField] float m_bobVerticalAmplitudeWhileRun;

//    CameraBobbing m_cameraBobbing;

//    void Awake()
//    {
//        m_cameraBobbing = GetComponent<CameraBobbing>();
//    }

//    void Start()
//    {
//        MainLinks.Instance.PlayerSpeed.OnPlayerRun += ChangeBobbingDuringRun;
//        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun += m_cameraBobbing.ResetBobbingValues;
//    }

//    void ChangeBobbingDuringRun()
//    {
//        m_cameraBobbing.BobFrequency = m_bobFrequencyWhileRun;
//        m_cameraBobbing.BobVerticalAmplitude = m_bobVerticalAmplitudeWhileRun;
//    }

//    void OnDisable()
//    {
//        MainLinks.Instance.PlayerSpeed.OnPlayerRun -= ChangeBobbingDuringRun;
//        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun -= m_cameraBobbing.ResetBobbingValues;
//    }

//}

class BobbingWhileRun : BobbingChangeWhileAction
{
    protected override void Subscribe()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerRun += ChangeBobbingDuringRun;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun += m_cameraBobbing.ResetBobbingValues;
    }

    protected override void Unsubscribe()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerRun -= ChangeBobbingDuringRun;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun -= m_cameraBobbing.ResetBobbingValues;
    }
}