using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class SoundOnAction : MonoBehaviour
{
    [SerializeField] protected AudioSource m_audioSource;

    void Awake()
    {
        SubscribeToAction();
    }

    protected virtual void PlaySound()
    {
        m_audioSource.Play();
    }

    protected virtual void StopSound()
    {
        m_audioSource.Stop();
    }

    void OnDestroy()
    {
        UnscribeToAction();
    }

    protected abstract void SubscribeToAction();
    protected abstract void UnscribeToAction();
}
