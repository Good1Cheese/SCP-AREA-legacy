using System;
using System.Collections;
using UnityEngine;

public class BleedingController : MonoBehaviour
{
    [SerializeField] float _delayBeforeHeal;
    [SerializeField] float _bleedDelay;
    [SerializeField] float _bleedDamage;
    bool _isBleeding;
    float _duration;
    WaitForSeconds _bleedWaitForSeconds;

    void Start()
    {
        _bleedWaitForSeconds = new WaitForSeconds(_bleedDelay);
    }

    void Update()
    {
        if (!_isBleeding) { return; }
        GetDuradurationOfPressingHealButton();

        if (_duration >= _delayBeforeHeal)
        {
            print("Bleeding Stopped");
            StopAllCoroutines();
            _isBleeding = false;
        }
    }

    public void Bleed() => StartCoroutine(BleedCoroutine());

    IEnumerator BleedCoroutine()
    {
        _isBleeding = true;
        var playerHealthController = MainLinks.Instance.PlayerHealthController;
        while (playerHealthController.Health > 0)
        {
            MainLinks.Instance.PlayerHealthController.Damage(_bleedDamage);
            yield return _bleedWaitForSeconds;
        }
        _isBleeding = false;
    }

    void GetDuradurationOfPressingHealButton()
    {
        if (Input.GetButton("Healing"))
        {
            _duration += Time.deltaTime;
            return;
        }
        _duration = 0;
    }
}