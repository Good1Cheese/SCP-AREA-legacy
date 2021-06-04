using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSaver : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }
}
