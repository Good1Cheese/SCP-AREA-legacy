using Zenject;

public class SlowWalkRun : Run
{
    public override void Use()
    {
        if (!_slowWalk.Using) { return; }

        UseRun();
    }

    protected override void Subscribe()
    {
        _inputContainer.SlowWalkRun.performed += Perform;
        _inputContainer.SlowWalkRun.canceled += Cancel;
    }

    protected override void Unsubscribe()
    {
        _inputContainer.SlowWalkRun.performed -= Perform;
        _inputContainer.SlowWalkRun.canceled -= Cancel;
    }
}