using System.Collections;
using UnityEngine;
using Zenject;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] float m_fovIncreasingSpeed;
    [SerializeField] float m_fovRestoreSpeed;
    [SerializeField] float m_delayDuringFovChange;
    [SerializeField] float m_fovDuringRun;
    [SerializeField] float m_startFov;

    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerMovement m_playerMovement;

    WaitForSeconds m_timeoutWhileFovChanging;
    Camera mainCamera;
    IEnumerator m_increaseCoroutine;
    IEnumerator m_resetCoroutine;

    void Awake()
    {
        mainCamera = Camera.main;
        mainCamera.fieldOfView = m_startFov;
        m_timeoutWhileFovChanging = new WaitForSeconds(m_delayDuringFovChange);
        m_increaseCoroutine = IncreaseFovCoroutine();
        m_resetCoroutine = ResetFovCoroutine();
    }

    void Start()
    {
        m_playerSpeed.OnPlayerRun += IncreaseFov;
        m_playerSpeed.OnPlayerStoppedRun += ResetFov;
        m_playerMovement.OnPlayerStoppedMoving += ResetFov;
    }

    void IncreaseFov() => StartCoroutine(m_increaseCoroutine);
    void ResetFov() => StartCoroutine(m_resetCoroutine);

    IEnumerator IncreaseFovCoroutine()
    {
        StopFovResettingCoroutine();
        while (mainCamera.fieldOfView <= m_fovDuringRun)
        {
            mainCamera.fieldOfView += m_fovIncreasingSpeed;
            yield return m_timeoutWhileFovChanging;
        }
    }

    IEnumerator ResetFovCoroutine()
    {
        StopFovIncreasingCoroutine();
        while (mainCamera.fieldOfView >= m_startFov)
        {
            mainCamera.fieldOfView -= m_fovRestoreSpeed;
            yield return m_timeoutWhileFovChanging;
        }
    }

    void StopFovIncreasingCoroutine()
    {
        StopCoroutine(m_increaseCoroutine);
        m_increaseCoroutine = IncreaseFovCoroutine();
    }

    void StopFovResettingCoroutine()
    {
        StopCoroutine(m_resetCoroutine);
        m_resetCoroutine = ResetFovCoroutine();
    }

    void OnDestroy()
    {
        m_playerSpeed.OnPlayerRun -= IncreaseFov;
        m_playerSpeed.OnPlayerStoppedRun -= ResetFov;
        m_playerMovement.OnPlayerStoppedMoving -= ResetFov;
    }
}
