using UnityEngine;
using Zenject;

public class PeekController : MonoBehaviour
{
    [SerializeField] private KeyCode _peekKey;

    [Inject] readonly private PlayerMovement _playerMovement;
    [Inject] readonly private MovementController _movementController;
    [Inject] readonly private SlowWalkController _slowWalkController;

    private VerticalPeek _verticalPeek;
    private HorizontalPeek _horizontalPeek;
    private bool _isPeekEnabled;

    private void Start()
    {
        _horizontalPeek = GetComponent<HorizontalPeek>();
        _verticalPeek = GetComponent<VerticalPeek>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_peekKey)) 
        { 
            _isPeekEnabled = !_isPeekEnabled;
            _playerMovement.enabled = !_isPeekEnabled;
            StopPlayer();
        }

        if (Input.GetKeyUp(_peekKey))
        {
            _isPeekEnabled = !_isPeekEnabled;
            _playerMovement.enabled = !_isPeekEnabled; 
        }
    }

    private void LateUpdate()
    {
        if (_isPeekEnabled)
        {
            _slowWalkController.GetSpeed();
            _verticalPeek.Peek();
            _horizontalPeek.Peek();

            return;
        }

        _verticalPeek.Restore();
        _horizontalPeek.Restore();
    }

    private void StopPlayer()
    {
        _horizontalPeek.PeekTime = 0;
        _verticalPeek.PeekTime = 0;
        _playerMovement.HorizontalMove = 0;
        _playerMovement.VerticalMove = 0;
        _movementController.MoveTime = 0;
    }
}