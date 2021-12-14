using UnityEngine;
using Zenject;

public class InteractionMarkEnablerDisabler : MonoBehaviour
{
    [SerializeField] private RectTransform _markCanvas;

    [Inject(Id = "Player")] private readonly Transform _playerTransform;
    private InteractionProvider _interactionProvider;
    private GameObject _canvasGameObject;

    private void Awake()
    {
        _interactionProvider = GetComponent<InteractionProvider>();
        _canvasGameObject = _markCanvas.gameObject;
    }

    private void Start()
    {
        _interactionProvider.InteractableFound += ActivateMark;
        _interactionProvider.UnInteractableFound += DisableMark;
    }

    public void ActivateMark(Collider collider)
    {
        _canvasGameObject.SetActive(true);
        _markCanvas.rotation = Quaternion.LookRotation(_playerTransform.position - _markCanvas.position);
        _markCanvas.position = collider.ClosestPoint(_playerTransform.position);
    }

    private void DisableMark()
    {
        _canvasGameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _interactionProvider.InteractableFound -= ActivateMark;
        _interactionProvider.UnInteractableFound -= DisableMark;
    }
}