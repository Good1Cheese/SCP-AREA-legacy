using UnityEngine;
using Zenject;

public class PlayerUIEnablerDisabler : MonoBehaviour
{
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;
    private GameObject _gameObject;

    [Inject]
    private void Construct(PauseMenuEnablerDisabler pauseMenuEnablerDisabler)
    {
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
    }

    private void Start()
    {
        _gameObject = gameObject;
        _pauseMenuEnablerDisabler.EnabledDisabled += SetActive;
    }

    private void SetActive() => _gameObject.SetActive(_gameObject.activeSelf);

    private void OnDestroy()
    {
        _pauseMenuEnablerDisabler.EnabledDisabled -= SetActive;
    }
}