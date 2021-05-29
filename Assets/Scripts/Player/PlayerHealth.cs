using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BleedingController))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth;
    BleedingController bleedingController;
    public float Health { get; set; }

    void Awake()
    {
        Health = maxHealth;
        MainLinks.Instance.PlayerHealthController = this;
        bleedingController = GetComponent<BleedingController>();
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
        bleedingController.BleedCaller();
    }

    void Die()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneChanger.Scenes.RespawnScene);
    }
}
