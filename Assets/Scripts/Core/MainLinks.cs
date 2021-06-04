using System;
using System.Collections;
using UnityEngine;

public class MainLinks : MonoBehaviour
{
    public static MainLinks Instance { get; private set; }
    public SceneTransition SceneChanger { get; set; }
    public Transform Camera { get; set; }
    public GameObject Player { get; set; }
    public PlayerStamina PlayerStamina { get; set; }
    public PlayerHealth PlayerHealthController { get; set; }
    public Action OnPlayerRunning { get; set; }
    public Action OnPlayerGetsDamage { get; set; }
    public bool IsPlayerRunning { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are not only one MainLinks's Instance");
        }
    }
}
