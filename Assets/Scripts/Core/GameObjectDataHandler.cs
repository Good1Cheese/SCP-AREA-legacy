using UnityEngine;

public class GameObjectDataHandler : DataHandler
{
    protected Transform m_transform;
    protected GameObject m_gameObject;

    public Transform _transform;
    public Vector3 position;
    public Quaternion rotation;
    public bool isGameObjectActive;

    void Awake()
    {
        m_transform = transform;
        m_gameObject = gameObject;
    }

    public override void SaveData()
    {
        _transform = m_transform;
        position = m_transform.position;
        rotation = m_transform.rotation;
        isGameObjectActive = m_gameObject.activeInHierarchy;
    }

    public override void LoadData()
    {
        _transform.SetPositionAndRotation(position, rotation);
        m_gameObject.SetActive(isGameObjectActive);
    }

}

