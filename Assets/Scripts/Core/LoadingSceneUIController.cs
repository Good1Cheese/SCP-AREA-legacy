using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingSceneUIController : MonoBehaviour
{
    [SerializeField] Slider m_loadSlider;
    [SerializeField] TextMeshProUGUI m_progressText;
    [SerializeField] GameObject m_gameObject;

    [Inject] readonly SceneTransition m_sceneTransition;

    public bool IsActiveStateConstant { get; set; }

    void Awake()
    {
        if (m_sceneTransition.LoadingSceneUIController == null) { return; }

        Destroy(m_sceneTransition.LoadingSceneUIController.gameObject);
    }

    void Start()
    {
        m_sceneTransition.LoadingSceneUIController = this;

        m_gameObject.SetActive(false);
    }

    public void SetActiveState(bool activeState)
    {
        if (IsActiveStateConstant) { return; }

        m_gameObject.SetActive(activeState);
    }



    public void UpdateUI(float progress)
    {
        m_loadSlider.value = progress;
        m_progressText.text = string.Format($"{progress * 100} %");
    }
}