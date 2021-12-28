using System;
using UnityEngine;

public class GameObjectTrigger : MonoBehaviour
{
    public bool IsTriggered { get; private set; }
    public Action TriggerStay { get; set; }
    public Action TriggerEnter { get; set; }
    public Action TriggerExit { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        TriggerEnter?.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        TriggerStay?.Invoke();
        IsTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        TriggerExit?.Invoke();
        IsTriggered = false;
    }
}