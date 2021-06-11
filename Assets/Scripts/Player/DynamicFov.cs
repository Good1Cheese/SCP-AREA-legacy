using System.Collections;
using UnityEngine;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] float m_fovIncreasingSpeed;
    [SerializeField] float m_fovRestoreSpeed;
    [SerializeField] float m_delayDuringFovChange;
    [SerializeField] float m_fovDuringRun;
    [SerializeField] float m_startFov;

    WaitForSeconds m_timeoutWhileFovChanging;

    void Awake()
    {
        Camera.main.fieldOfView = m_startFov;
        m_timeoutWhileFovChanging = new WaitForSeconds(m_delayDuringFovChange);
    }

    void Start()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerRun += IncreaseFov;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun += ResetFov;
    }

    void IncreaseFov() => StartCoroutine(IncreaseFovCoroutine());

    void ResetFov() => StartCoroutine(ResetFovCoroutine());

    void Update()
    {
        if (MainLinks.Instance.PlayerStamina.HasPlayer≈noughStaminaToRun) { return; }
        ResetFov();
    }

    IEnumerator IncreaseFovCoroutine()
    {
        while (Camera.main.fieldOfView <= m_fovDuringRun && MainLinks.Instance.PlayerStamina.IsPlayerRunning)
        {
            Camera.main.fieldOfView += m_fovIncreasingSpeed;
            yield return m_timeoutWhileFovChanging;
        }
    }

    IEnumerator ResetFovCoroutine()
    {
        while (Camera.main.fieldOfView >= m_startFov)
        {
            Camera.main.fieldOfView -= m_fovRestoreSpeed;
            yield return m_timeoutWhileFovChanging;
        }
    }

    void OnDisable()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerRun -= IncreaseFov;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun -= ResetFov;
    }
}
