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
    Camera mainCamera;
    IEnumerator m_increaseCoroutine;
    IEnumerator m_resetCoroutine;

    void Awake()
    {
        mainCamera = Camera.main;
        mainCamera.fieldOfView = m_startFov;
        m_timeoutWhileFovChanging = new WaitForSeconds(m_delayDuringFovChange);
    }

    void Start()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerRun += IncreaseFov;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun += ResetFov;
        m_increaseCoroutine = IncreaseFovCoroutine();
        m_resetCoroutine = ResetFovCoroutine();
    }

    void IncreaseFov() => StartCoroutine(m_increaseCoroutine);
    void ResetFov() => StartCoroutine(m_resetCoroutine);

    IEnumerator IncreaseFovCoroutine()
    {
        StopResetCoroutine();
        while (mainCamera.fieldOfView <= m_fovDuringRun)
        {
            mainCamera.fieldOfView += m_fovIncreasingSpeed;
            yield return m_timeoutWhileFovChanging;
        }
    }

    IEnumerator ResetFovCoroutine()
    {
        StopIncreaseCoroutine();
        while (mainCamera.fieldOfView >= m_startFov)
        {
            mainCamera.fieldOfView -= m_fovRestoreSpeed;
            yield return m_timeoutWhileFovChanging;
        }
    }

    void StopIncreaseCoroutine()
    {
        StopCoroutine(m_increaseCoroutine);
        m_increaseCoroutine = IncreaseFovCoroutine();
    }

    void StopResetCoroutine()
    {
        StopCoroutine(m_resetCoroutine);
        m_resetCoroutine = ResetFovCoroutine();
    }

    void OnDisable()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerRun -= IncreaseFov;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun -= ResetFov;
    }
}
