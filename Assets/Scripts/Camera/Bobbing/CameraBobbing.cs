﻿using UnityEngine;

[RequireComponent(typeof(BobbingWhileRun))]
class CameraBobbing : MonoBehaviour
{
    [SerializeField] float m_bobFrequency;
    [SerializeField] float m_bobHorizontalAmplitude;
    [SerializeField] float m_bobVerticalAmplitude;
    [SerializeField] [Range(0, 1)] float m_headBobSmoothing;

    float m_walkingTime;
    Transform m_cameraTransform;
    (float, float) m_startValuesOfChangableFields;

    public float BobFrequency { get => m_bobFrequency; set => m_bobFrequency = value; }
    public float BobVerticalAmplitude { get => m_bobVerticalAmplitude; set => m_bobVerticalAmplitude = value; }

    void Start()
    {
        m_startValuesOfChangableFields = (m_bobFrequency, m_bobVerticalAmplitude);
        m_cameraTransform = Camera.main.transform;
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
        m_cameraTransform.position = Vector3.Lerp(m_cameraTransform.position, targetCameraPosition, m_headBobSmoothing);

        if ((m_cameraTransform.position - targetCameraPosition).magnitude <= 0.001) m_cameraTransform.position = targetCameraPosition;
    }

    void ResetTime()
    {
        m_walkingTime = 0;
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

    public void ResetBobbingValues()
    {
        m_bobFrequency = m_startValuesOfChangableFields.Item1;
        m_bobVerticalAmplitude = m_startValuesOfChangableFields.Item2;
    }
}

