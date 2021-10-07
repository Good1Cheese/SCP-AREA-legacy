using UnityEngine;

public class ObjectSaver : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);    
    }
}
