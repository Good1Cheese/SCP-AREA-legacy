using UnityEngine;
using Zenject;

public class LeanController : MonoBehaviour
{
    [SerializeField] private KeyCode _key;

    [Inject] readonly private PlayerMovement _playerMovement;
    [Inject] readonly private MovementController _movementController;
    [Inject] readonly private SlowWalkController _slowWalkController;

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
            _playerMovement.enabled = !_isPeekEnabled;
            StopPlayer();
        }

        if (Input.GetKeyUp(_key))
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
            _verticalLean.Lean();
            _horizontalLean.Lean();

            return;
        }

        _verticalLean.Restore();
        _horizontalLean.Restore();
    }

    private void StopPlayer()
    {
        _horizontalLean.LeanTime = 0;
        _verticalLean.LeanTime = 0;
        _playerMovement.HorizontalMove = 0;
        _playerMovement.VerticalMove = 0;
        _movementController.MoveTime = 0;
    }
}