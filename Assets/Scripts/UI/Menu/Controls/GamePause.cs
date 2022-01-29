using UnityEngine;
using Zenject;

[RequireComponent(typeof(PauseMenuControls))]
public class GamePause : MonoBehaviour
{
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private GameObject _gameObject;
    private PauseMenuControls _pauseMenuControls;

    [Inject]
    private void Construct(PauseMenuEnablerDisabler pauseMenuEnablerDisabler)
    {
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _pauseMenuControls = GetComponent<PauseMenuControls>();

        _pauseMenuEnablerDisabler.EnabledDisabled += ActivateOrDeacrivateUI;
        _pauseMenuControls.Exited += OnExited;
    }

    public void ActivateOrDeacrivateUI()
    {
        Time.timeScale = (_gameObject.activeSelf) ? 1 : 0;
        _gameObject.SetActive(!_gameObject.activeSelf);
    }

    public void OnExited()
    {
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        _pauseMenuEnablerDisabler.EnabledDisabled -= ActivateOrDeacrivateUI;
        _pauseMenuControls.Exited -= OnExited;
    }
}