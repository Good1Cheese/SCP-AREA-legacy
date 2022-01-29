using UnityEngine;

public class GameObjectSaving : DataSaving
{
    protected Transform _cachedTransform;
    protected GameObject _gameObject;

    public Transform _transform;
    public Vector3 position;
    public Quaternion rotation;
    public bool isActive;

    private void Awake()
    {
        _cachedTransform = transform;
        _gameObject = gameObject;
    }

    private void Start()
    {
        _gameSaving.SaveData.Add(this);
    }

    public override void Save()
    {
        _transform = _cachedTransform;
        position = _transform.position;
        rotation = _transform.rotation;
        isActive = _gameObject.activeInHierarchy;
    }

    public override void Load()
    {
        _transform.SetPositionAndRotation(position, rotation);
        _gameObject.SetActive(isActive);
    }

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        _transform = _cachedTransform;
        Load();
    }
}
