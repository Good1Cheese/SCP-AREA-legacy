using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingSceneUIController : MonoBehaviour
{
    [SerializeField] private Slider _loadSlider;
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private GameObject _gameObject;

    private SceneTransition _sceneTransition;

    [Inject]
    private void Construct(SceneTransition sceneTransition)
    {
        _sceneTransition = sceneTransition;
    }

    public bool IsActiveStateConstant { get; set; }

    private void Start()
    {
        if (_sceneTransition.LoadingSceneUIController != null)
        {
            Destroy(_sceneTransition.LoadingSceneUIController.gameObject);
        }

        _sceneTransition.LoadingSceneUIController = this;

        _gameObject.SetActive(false);
    }

    public void SetActiveState(bool activeState)
    {
        if (IsActiveStateConstant) { return; }

        _gameObject.SetActive(activeState);
    }

    public void UpdateUI(float progress)
    {
        _loadSlider.value = progress;
        _progressText.text = string.Format($"{Mathf.Round(progress * 100)} %");
    }
}