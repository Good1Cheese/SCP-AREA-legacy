using UnityEngine;
using Zenject;

public class PlayerDeathCloneActivator : MonoBehaviour
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    DeathAnimationPlayer m_playerDeathAnimation;
    GameObject m_gameObject;

    void Awake()
    {
        m_gameObject = transform.parent.gameObject;
        m_playerDeathAnimation = GetComponent<DeathAnimationPlayer>();
    }

    void Start()
    {
        m_playerHealth.OnPlayerDies += ActivateDeathAnimation;
        m_gameObject.SetActive(false);
    }

    void ActivateDeathAnimation()
    {
        m_gameObject.transform.SetPositionAndRotation(m_playerTransform.position, m_playerTransform.rotation);
        m_gameObject.SetActive(true);

        m_playerDeathAnimation.PlayDeathAnimation();
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerDies -= ActivateDeathAnimation;
    }
}
