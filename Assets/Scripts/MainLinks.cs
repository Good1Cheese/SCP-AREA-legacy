using System;
using UnityEngine;

public class MainLinks : MonoBehaviour
{
    public static MainLinks Instance { get; private set; }
    public Action OnPlayerRunning { get; set; }

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
