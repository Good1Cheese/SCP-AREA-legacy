using UnityEngine;

public class GameObjectDataHandler : DataHandler
{
    protected Transform m_transform;
    protected GameObject m_gameObject;

    public Transform _transform;
    public Vector3 position;
    public Quaternion rotation;
    public bool isGameObjectActive;

    void Start()
    {
        m_gameSaving.SaveData.Add(this);
    }
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

    public override void FromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        _transform = m_transform;
        LoadData();
    }
}
