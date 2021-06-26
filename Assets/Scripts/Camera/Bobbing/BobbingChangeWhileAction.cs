using UnityEngine;

abstract class BobbingChangeWhileAction : MonoBehaviour
{
    [SerializeField] float m_bobFrequencyWhileAction;
    [SerializeField] float m_bobVerticalAmplitudeWhileAction;

    protected CameraBobbing m_cameraBobbing;

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();

    void Start()
    {
        m_cameraBobbing = GetComponent<CameraBobbing>();
        Subscribe();
    }

    protected void ChangeBobbingDuringRun()
    {
        m_cameraBobbing.BobFrequency = m_bobFrequencyWhileAction;
        m_cameraBobbing.BobVerticalAmplitude = m_bobVerticalAmplitudeWhileAction;
    }

    void OnDestroy()
    {
        Unsubscribe();
    }

}

