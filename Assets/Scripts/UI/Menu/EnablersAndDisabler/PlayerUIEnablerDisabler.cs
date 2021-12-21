using UnityEngine;
using Zenject;

public class PlayerUIEnablerDisabler : MonoBehaviour
{
    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenu;
    private GameObject _gameObject;

    private void Start()
    {
        _gameObject = gameObject;
        _pauseMenu.EnabledDisabled += SetActive;
    }

    private void SetActive()
    {
        _gameObject.SetActive(_gameObject.activeSelf);
    }

    private void OnDestroy()
    {
        _pauseMenu.EnabledDisabled -= SetActive;
    }
}