using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class SoundPlayerOnAction : MonoBehaviour
{
    [SerializeField] protected AudioSource audioSource;

    void Awake()
    {
        SubscribeToAction();
    }

    protected virtual void PlaySound()
    {
        audioSource.Play();
    }

    protected void StopSound()
    {
        audioSource.Stop();
    }

    void OnDestroy()
    {
        UnscribeToAction();
    }

    protected abstract void SubscribeToAction();
    protected abstract void UnscribeToAction();

}
