using UnityEngine;

public abstract class BobbingChangeWhileMoveAction : MonoBehaviour
{
    [SerializeField] float m_bobFrequencyWhileAction;
    [SerializeField] float m_bobVerticalAmplitudeWhileAction;

    protected CameraBobbing m_cameraBobbing;
    protected MoveController m_moveController;

    void Start()
    {
        m_cameraBobbing = GetComponent<CameraBobbing>();

        m_moveController.OnPlayerStartedUseOfMove += ChangeBobbingDuringRun;
        m_moveController.OnPlayerStoppedUseOfMove += m_cameraBobbing.ResetBobbingValues;
    }

    protected void ChangeBobbingDuringRun()
    {
        m_cameraBobbing.BobFrequency = m_bobFrequencyWhileAction;
        m_cameraBobbing.BobVerticalAmplitude = m_bobVerticalAmplitudeWhileAction;
    }

    void OnDestroy()
    {
        m_moveController.OnPlayerStartedUseOfMove -= ChangeBobbingDuringRun;
        m_moveController.OnPlayerStoppedUseOfMove -= m_cameraBobbing.ResetBobbingValues;
    }
}

