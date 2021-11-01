using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerWalkSound), typeof(PlayerRunSound))]
public class MovementSpeed : MonoBehaviour
{
    [SerializeField] AnimationCurve m_runSpeed;

    [SerializeField] float m_walkSpeed;

    [Inject] readonly PlayerStamina m_playerStamina;

    float m_slowDownFactor;

    public Action OnPlayerRunning { get; set; }
    public Action OnPlayerNotRunning { get; set; }
    public Action OnPlayerStartedRun { get; set; }
    public Action OnPlayerStoppedRun { get; set; }
    public Action OnPlayerWalks { get; set; }

    public bool IsPlayerRunning { get; set; }
    public float RunTime { get; set; }

    public float GetPlayerSpeed()
    {
        StopMovement();
        return GetSpecialSpeed();
    }

    float GetSpecialSpeed()
    {
        if (Input.GetButtonDown("Sprint") && m_playerStamina.HasPlayerStamina)
        {
            OnPlayerStartedRun?.Invoke();
        }

        if (Input.GetButton("Sprint") && m_playerStamina.HasPlayerStamina)
        {
            RunTime += Time.deltaTime;

            OnPlayerRunning?.Invoke();
            IsPlayerRunning = true;

            return m_runSpeed.Evaluate(RunTime) - m_slowDownFactor;
        }
        else
        {
            if (RunTime > 0)
            {
                RunTime -= Time.deltaTime;
            }

            OnPlayerNotRunning?.Invoke();
        }

        OnPlayerWalks?.Invoke();
        return m_walkSpeed - m_slowDownFactor;
    }

    void StopMovement()
    {
        if (Input.GetButtonUp("Sprint"))
        {
            Keyframe lastKeyframe = m_runSpeed.keys[m_runSpeed.keys.Length - 1];
            if (RunTime > lastKeyframe.time)
            {
                RunTime = lastKeyframe.time;
            }

            OnPlayerStoppedRun?.Invoke();
            IsPlayerRunning = false;
        }
    }

    public void SlowDownSpeed(float slowDownFactor)
    {
        m_slowDownFactor = slowDownFactor;
    }
}
