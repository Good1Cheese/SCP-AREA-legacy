using UnityEngine;

class CameraBobbing : MonoBehaviour
{
    [SerializeField] float m_bobFrequency;
    [SerializeField] float m_bobHorizontalAmplitude;
    [SerializeField] float m_bobVerticalAmplitude;
    [SerializeField] [Range(0, 1)] float m_headBobSmoothing;

    float m_walkingTime;
    Transform m_cameraPosition;

    void Start()
    {
        m_cameraPosition = MainLinks.Instance.Camera;
    }

    void Update()
    {
        if (!IsPlayerMoving())
        {
            m_walkingTime = 0;
            return;
        }

        m_walkingTime += Time.deltaTime;

        Vector3 targetCameraPosition = transform.position + CalculateHeadBobbingOffset(m_walkingTime);
        MainLinks.Instance.Camera.position = Vector3.Lerp(MainLinks.Instance.Camera.position, targetCameraPosition, m_headBobSmoothing);

        if ((MainLinks.Instance.Camera.position - targetCameraPosition).magnitude <= 0.001) MainLinks.Instance.Camera.position = targetCameraPosition;
    }

    Vector3 CalculateHeadBobbingOffset(float time)
    {
        float horizontalOffset = Mathf.Sin(time * m_bobFrequency) * m_bobHorizontalAmplitude;
        float verticalOffset = Mathf.Cos(time * m_bobFrequency * 2) * m_bobVerticalAmplitude;
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

