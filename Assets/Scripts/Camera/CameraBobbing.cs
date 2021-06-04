using UnityEngine;

class CameraBobbing : MonoBehaviour
{
    [SerializeField] float _bobFrequency;
    [SerializeField] float _bobHorizontalAmplitude;
    [SerializeField] float _bobVerticalAmplitude;
    [SerializeField] [Range(0, 1)] float _headBobSmoothing;

    float walkingTime;
    Transform cameraPosition;

    void Start()
    {
        cameraPosition = MainLinks.Instance.Camera;
    }

    void Update()
    {
        if (!IsPlayerMoving())
        {
            walkingTime = 0;
            return;
        }

        walkingTime += Time.deltaTime;

        Vector3 targetCameraPosition = transform.position + CalculateHeadBobbingOffset(walkingTime);
        MainLinks.Instance.Camera.position = Vector3.Lerp(MainLinks.Instance.Camera.position, targetCameraPosition, _headBobSmoothing);

        if ((MainLinks.Instance.Camera.position - targetCameraPosition).magnitude <= 0.001) MainLinks.Instance.Camera.position = targetCameraPosition;
    }

    Vector3 CalculateHeadBobbingOffset(float time)
    {
        float horizontalOffset = Mathf.Sin(time * _bobFrequency) * _bobHorizontalAmplitude;
        float verticalOffset = Mathf.Cos(time * _bobFrequency * 2) * _bobVerticalAmplitude;
        Vector3 offset = transform.right * horizontalOffset + transform.up * verticalOffset;

        return offset;
    }

    bool IsPlayerMoving()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        return horizontalMove != 0 || verticalMove != 0;
    }
}

