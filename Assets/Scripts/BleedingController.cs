using System;
using System.Collections;
using UnityEngine;

public class BleedingController : MonoBehaviour
{
    [SerializeField] float requiredDurationToHeal;
    [SerializeField] float bleedDelay;
    [SerializeField] float bleedDamage;
    bool isBleeding;
    float duration;


    void Update()
    {
        if (!isBleeding) { return; }
        GetDuradurationOfPressingHealButton();

        if (duration >= requiredDurationToHeal)
        {
            print("Bleeding Stopped");
            StopAllCoroutines();
            isBleeding = false;
        }
    }

    public void BleedCaller() => StartCoroutine(Bleed());

    IEnumerator Bleed()
    {
        isBleeding = true;
        var playerHealthController = MainLinks.Instance.PlayerHealthController;
        while (playerHealthController.Health > 0)
        {
            MainLinks.Instance.PlayerHealthController.Damage(bleedDamage);
            yield return new WaitForSeconds(bleedDelay);
        }
        isBleeding = false;
    }

    void GetDuradurationOfPressingHealButton()
    {
        if (Input.GetButton("Healing"))
        {
            duration += Time.deltaTime;
            return;
        }
        duration = 0;
    }
}