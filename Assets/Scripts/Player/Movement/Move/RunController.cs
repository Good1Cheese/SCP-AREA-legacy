﻿using Zenject;

public class RunController : MoveController
{
    [Inject] private readonly PlayerStamina _playerStamina;
    [Inject] protected readonly SlowWalkController _slowWalkController;
    [Inject] protected readonly SlowWalkRunController _slowWalkRunController;
    [Inject] private readonly PlayerMovement _playerMovement;

    public override float GetMove()
    {
        if (_slowWalkController.IsMoving) { return 0; }

        if (_slowWalkRunController.IsMoving)
        {
            UseStarted?.Invoke();
        }

        return Run();
    }

    protected float Run()
    {
        if (_playerMovement.VerticalMove < 0) { return 0; }

        if (_playerStamina.Amount <= 0)
        {
            return 0;
        }

        return base.GetMove();
    }
}