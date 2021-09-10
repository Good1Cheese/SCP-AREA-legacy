using UnityEngine;
using Zenject;

public class PlayerDeath : MonoBehaviour
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly GameObject m_playerGameObject;
    [Inject] readonly Transform m_playerTransform;

    DeathAnimationPlayer m_playerDeathAnimation;
    GameObject m_gameObject;

    void Awake()
    {
        m_gameObject = transform.parent.gameObject;
        m_playerDeathAnimation = GetComponent<DeathAnimationPlayer>();
    }

    void Start()
    {
        m_playerHealth.OnPlayerDie += ActivateDeathAnimation;
        m_gameObject.SetActive(false);
    }

    private void ActivateDeathAnimation()
    {
        m_playerGameObject.SetActive(false);
        m_gameObject.transform.SetPositionAndRotation(m_playerTransform.position, m_playerTransform.rotation);
        m_gameObject.SetActive(true);

        m_playerDeathAnimation.PlayDealthAnimation();
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerDie -= ActivateDeathAnimation;
    }
}
