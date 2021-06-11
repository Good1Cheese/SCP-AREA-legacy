using System;
using System.Collections;
using UnityEngine;

public class MainLinks : MonoBehaviour
{
    public static MainLinks Instance { get; private set; }
    public SceneTransition SceneChanger { get; set; }
    public Transform Camera { get; set; }
    public PlayerStamina PlayerStamina { get; set; }
    public PlayerHealth PlayerHealth { get; set; }
    public PlayerSpeed PlayerSpeed { get; set; }
    public CharacterBleeding PlayerBleeding { get; set; }

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
