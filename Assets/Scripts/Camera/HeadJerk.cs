using UnityEngine;
using Zenject;

public class HeadJerk : MonoBehaviour
{
    [SerializeField] Animator m_animator;

    [Inject]
    void Construct(WeaponFire weaponFire)
    {
        weaponFire.OnPlayerShooted += ActivateJerk;
    }

    void ActivateJerk()
    {
        m_animator.SetTrigger("OnPlayerShooted");
    }
}
