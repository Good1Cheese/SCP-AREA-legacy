using UnityEngine;

[RequireComponent(typeof(BleedingController))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float m_maxHealth;
    BleedingController m_bleedingController;
    public float Health { get; set; }

    void Awake()
    {
        Health = m_maxHealth;
        m_bleedingController = GetComponent<BleedingController>();
    }

    void Start()
    {
        MainLinks.Instance.PlayerHealthController = this;
    }

    public void Damage(float amoutOfDamage)
    {
        Health -= amoutOfDamage;
        if (Health > 0)
        {
            MainLinks.Instance.OnPlayerGetsDamage?.Invoke();
            return;
        }
        Die();
    }

    public void Scratch(float amoutOfDamage)
    {
        Damage(amoutOfDamage);
        m_bleedingController.Bleed();
    }

    void Die()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}
