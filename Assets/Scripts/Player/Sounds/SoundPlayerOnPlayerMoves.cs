using UnityEngine;
using Zenject;

public class SoundPlayerOnPlayerMoves : MonoBehaviour
{
    [Inject] PlayerMovement m_playerMovement;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (m_playerMovement.IsPlayerMoving)
        {
            if (audioSource.isPlaying) { return; }
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

}
