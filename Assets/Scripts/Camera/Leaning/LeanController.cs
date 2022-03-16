using UnityEngine;
using Zenject;

public class LeanController : MonoBehaviour
{
    [SerializeField] private KeyCode _key;

    [Inject] readonly private MovementInputLink _movementInputLink;
    [Inject] readonly private MovesContainer _movesContainer;
    [Inject] readonly private SlowWalk _slowWalk;

    private VerticalLean _verticalLean;
    private HorizontalLean _horizontalLean;
    private bool _isPeekEnabled;

    private void Start()
    {
        _horizontalLean = GetComponent<HorizontalLean>();
        _verticalLean = GetComponent<VerticalLean>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            _isPeekEnabled = !_isPeekEnabled;
            _movementInputLink.enabled = !_isPeekEnabled;
            StopPlayer();
        }

        if (Input.GetKeyUp(_key))
        {
            _isPeekEnabled = !_isPeekEnabled;
            _movementInputLink.enabled = !_isPeekEnabled;
        }
    }

    private void LateUpdate()
    {
        if (_isPeekEnabled)
        {
            _slowWalk.Use();
            _verticalLean.Lean();
            _horizontalLean.Lean();

            return;
        }

        _verticalLean.Restore();
        _horizontalLean.Restore();
    }

    private void StopPlayer()
    {
        _horizontalLean.CurveTime = 0;
        _verticalLean.CurveTime = 0;
        _movesContainer.MoveTime = 0;
        _movementInputLink.HorizontalMove = 0;
        _movementInputLink.VerticalMove = 0;
    }
}