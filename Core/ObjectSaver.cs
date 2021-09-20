using UnityEngine;

public class ObjectSaver : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }
}
