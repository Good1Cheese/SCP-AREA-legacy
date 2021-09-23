using UnityEngine;

public class GameObjectDataSaving : DataSaving
{
    protected Transform m_transform;
    protected GameObject m_gameObject;

    public Transform _transform;
    public Vector3 position;
    public Quaternion rotation;
    public bool isActive;

    void Awake()
    {
        m_transform = transform;
        m_gameObject = gameObject;
    }

    void Start()
    {
        m_gameSaving.SaveData.Add(this);
    }

    public override void Save()
    {
        _transform = m_transform;
        position = m_transform.position;
        rotation = m_transform.rotation;
        isActive = m_gameObject.activeSelf;
    }

    public override void Load()
    {
        _transform.SetPositionAndRotation(position, rotation);
        m_gameObject.SetActive(isActive);
    }

    public override void LoadDataFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        _transform = m_transform;
        Load();
    }
}
