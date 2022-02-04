using UnityEngine;
using Zenject;

public class PlayerUIToggler : MonoBehaviour
{
    private PauseMenuToggler _pauseMenuToggler;
    private GameObject _gameObject;

    [Inject]
    private void Construct(PauseMenuToggler pauseMenuToggler)
    {
        _pauseMenuToggler = pauseMenuToggler;
    }

    private void Start()
    {
        _gameObject = gameObject;
        _pauseMenuToggler.Toggled += SetActive;
    }

    private void SetActive() => _gameObject.SetActive(_gameObject.activeSelf);

    private void OnDestroy()
    {
        _pauseMenuToggler.Toggled -= SetActive;
    }
}