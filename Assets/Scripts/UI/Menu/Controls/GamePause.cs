using UnityEngine;
using Zenject;

[RequireComponent(typeof(PauseMenuControls))]
public class GamePause : MonoBehaviour
{
    private PauseMenuToggler _pauseMenuToggler;
    private GameObject _gameObject;
    private PauseMenuControls _pauseMenuControls;

    [Inject]
    private void Construct(PauseMenuToggler pauseMenuToggler)
    {
        _pauseMenuToggler = pauseMenuToggler;
    }

    private void Awake()
    {
        _gameObject = gameObject;
        _pauseMenuControls = GetComponent<PauseMenuControls>();

        _pauseMenuToggler.Toggled += ActivateOrDeacrivateUI;
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
        _pauseMenuToggler.Toggled -= ActivateOrDeacrivateUI;
        _pauseMenuControls.Exited -= OnExited;
    }
}