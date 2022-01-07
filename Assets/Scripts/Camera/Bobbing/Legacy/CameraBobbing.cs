using UnityEngine;
using Zenject;

[RequireComponent(typeof(BobbingWhileRun))]
public class CameraBobbing : MonoBehaviour
{
    [SerializeField] private float _bobFrequency;
    [SerializeField] private float _bobHorizontalAmplitude;
    [SerializeField] private float _bobVerticalAmplitude;
    [SerializeField] [Range(0, 1)] private float _headBobSmoothing;

    [Inject] private readonly Camera _mainCamera;
    [Inject] private readonly PlayerMovement _playerMovement;

    private float _walkingTime;
    private (float, float) _startValuesOfChangableFields;

    public float BobFrequency { get => _bobFrequency; set => _bobFrequency = value; }
    public float BobVerticalAmplitude { get => _bobVerticalAmplitude; set => _bobVerticalAmplitude = value; }

    private void Start()
    {
        _startValuesOfChangableFields = (_bobFrequency, _bobVerticalAmplitude);
    }

    private void Update()
    {
        if (!IsPlayerMoving())
        {
            _walkingTime = 0;
            return;
        }

        _walkingTime += Time.deltaTime;

        Vector3 targetCameraPosition = transform.position + CalculateHeadBobbingOffset(_walkingTime);
        _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, targetCameraPosition, _headBobSmoothing);

        if ((_mainCamera.transform.position - targetCameraPosition).magnitude <= 0.001)
        {
            _mainCamera.transform.position = targetCameraPosition;
        }
    }

    private Vector3 CalculateHeadBobbingOffset(float time)
    {
        float horizontalOffset = Mathf.Sin(time * _bobFrequency) * _bobHorizontalAmplitude;
        float verticalOffset = Mathf.Cos(time * _bobFrequency * 2) * _bobVerticalAmplitude;
        Vector3 offset = transform.right * horizontalOffset + transform.up * verticalOffset;

        return offset;
    }

    private bool IsPlayerMoving() => _playerMovement.HorizontalMove != 0 || _playerMovement.VerticalMove != 0;

    public void ResetBobbingValues()
    {
        _bobFrequency = _startValuesOfChangableFields.Item1;
        _bobVerticalAmplitude = _startValuesOfChangableFields.Item2;
    }
}