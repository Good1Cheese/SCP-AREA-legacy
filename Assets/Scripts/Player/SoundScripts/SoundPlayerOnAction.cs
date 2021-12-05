using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class SoundOnAction : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioSource;

    private void Awake()
    {
        SubscribeToAction();
    }

    protected virtual void PlaySound()
    {
        _audioSource.Play();
    }

    protected virtual void StopSound()
    {
        _audioSource.Stop();
    }

    protected void OnDestroy()
    {
        UnscribeToAction();
    }

    protected abstract void SubscribeToAction();
    protected abstract void UnscribeToAction();
}