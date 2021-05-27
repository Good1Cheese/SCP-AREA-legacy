using UnityEngine;

[RequireComponent(typeof(BleedingController))]
public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] float health;
    BleedingController bleedingController;
    public float Health { get => health; set => health = value; }

    void Start()
    {
        MainLinks.Instance.Player = gameObject;
        MainLinks.Instance.PlayerHealthController = this;
        bleedingController = GetComponent<BleedingController>();
    }

    public void Damage(float amoutOfDamage)
    {
        health -= amoutOfDamage;
        if (health > 0)
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
        gameObject.SetActive(false);
    }
}
