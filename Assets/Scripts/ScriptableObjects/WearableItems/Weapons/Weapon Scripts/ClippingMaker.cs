using UnityEngine;

public class ClippingMaker : MonoBehaviour
{
    [SerializeField] private GameObjectTrigger _gameObjectTrigger;
    [SerializeField] private int _maxCameraXRotation;
    [SerializeField] private float _clippingSmoothing;
    [SerializeField] private float _clippingReturnSmoothing;
    [SerializeField] private Vector3 _clippingPosition;

    private Transform _mainCamera;
    private Vector3 _zero = Vector3.zero;
    private bool _isTriggered;
    private Transform _parentTransform;

    public GameObjectTrigger GameObjectTrigger { get => _gameObjectTrigger; }
    public Transform ParentTransform { set => _parentTransform = value; }
    public Transform MainCamera { set => _mainCamera = value; }
    public WeaponAim WeaponAim { get; set; }

    private void Start()
    {
        GameObjectTrigger.TriggerExit += Aim;
    }

    private void Aim()
    {
        if (!WeaponAim.WasAimed) { return; }

        _parentTransform.localPosition = _zero;
        WeaponAim.SetAimState(true);
        WeaponAim.WasAimed = false;
    }

    private void Update()
    {
        if (!_isTriggered || _mainCamera.rotation.eulerAngles.x >= _maxCameraXRotation) { return; }

        _parentTransform.localPosition = Vector3.Lerp(_parentTransform.localPosition, _clippingPosition, _clippingSmoothing * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (GameObjectTrigger.IsTriggered) { return; }

        _parentTransform.localPosition = Vector3.Lerp(_parentTransform.localPosition, _zero, _clippingReturnSmoothing * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        _isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) { return; }

        _isTriggered = false;
    }

    private void OnDestroy()
    {
        GameObjectTrigger.TriggerExit -= Aim;
    }
}