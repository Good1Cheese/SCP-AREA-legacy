using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestScirpt : MonoBehaviour
{
    [Inject] GameLoading gameSaving;

    void Start()
    {
        gameSaving.OnGameLoaded += DestroyTs;
    }

    private void DestroyTs()
    {
        Destroy(gameObject);
        gameSaving.OnGameLoaded -= DestroyTs;
    }


}
