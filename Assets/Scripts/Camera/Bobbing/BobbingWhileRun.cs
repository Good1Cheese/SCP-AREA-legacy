//using UnityEngine;

//class BobbingWhileRun : MonoBehaviour
//{
//    [SerializeField] float m_bobFrequencyWhileRun;
//    [SerializeField] float m_bobVerticalAmplitudeWhileRun;

//    CameraBobbing m_cameraBobbing;
using Zenject;

class BobbingWhileRun : BobbingChangeWhileAction
{
    [Inject] PlayerSpeed m_playerSpeed;

    protected override void Subscribe()
    {
        m_playerSpeed.OnPlayerRun += ChangeBobbingDuringRun;
        m_playerSpeed.OnPlayerStoppedRun += m_cameraBobbing.ResetBobbingValues;
    }

    protected override void Unsubscribe()
    {
        m_playerSpeed.OnPlayerRun -= ChangeBobbingDuringRun;
        m_playerSpeed.OnPlayerStoppedRun -= m_cameraBobbing.ResetBobbingValues;
    }
}