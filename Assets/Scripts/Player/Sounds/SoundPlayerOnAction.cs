using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class SoundPlayerOnAction : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SubscribeToAction();
    }

    protected void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void OnDestroy()
    {
        UnsubscribeToAction();
    }

    protected abstract void SubscribeToAction();
    protected abstract void UnsubscribeToAction();

}
