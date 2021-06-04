using UnityEngine;

[RequireComponent(typeof(BleedingController))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth;
    BleedingController _bleedingController;
    public float Health { get; set; }

    void Awake()
    {
        Health = _maxHealth;
        MainLinks.Instance.PlayerHealthController = this;
        _bleedingController = GetComponent<BleedingController>();
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
        _bleedingController.Bleed();
    }

    void Die()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}
