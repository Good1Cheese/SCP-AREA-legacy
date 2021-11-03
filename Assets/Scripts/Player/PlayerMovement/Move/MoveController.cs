using System;
using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    [SerializeField] KeyCode m_key;
    [SerializeField] protected AnimationCurve m_moveSpeed;

    public float MoveTime { get; set; }

    public Action OnPlayerUsingMove { get; set; }
    public Action OnPlayerNotUsingMove { get; set; }
    public Action OnPlayerStartedUseOfMove { get; set; }
    public Action OnPlayerStoppedUseOfMove { get; set; }

    public virtual float GetMove()
    {
        if (Input.GetKeyDown(m_key))
        {
            OnPlayerStartedUseOfMove?.Invoke();
        }

        if (Input.GetKey(m_key))
        {
            MoveTime += Time.deltaTime;

            OnPlayerUsingMove?.Invoke();

            return m_moveSpeed.Evaluate(MoveTime);
        }
        else
        {
            if (MoveTime > 0)
            {
                MoveTime -= Time.deltaTime;
            }

            OnPlayerNotUsingMove?.Invoke();
        }

        return 0;
    }

    public virtual void StopMove()
    {
        if (Input.GetKeyUp(m_key))
        {
            Keyframe lastKeyframe = m_moveSpeed.keys[m_moveSpeed.keys.Length - 1];

            if (MoveTime > lastKeyframe.time)
            {
                MoveTime = lastKeyframe.time;
            }

            OnPlayerStoppedUseOfMove?.Invoke();
        }

    }
}
