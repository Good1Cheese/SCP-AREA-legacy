using Zenject;

public class RightStepHeadbob : MovementHeadbob
{
    [Inject] private readonly WalkController _walkController;

    protected new void Awake()
    {
        base.Awake();
        _walkController.OnRightStep += ActivateHeadbob;
    }

    private void OnDestroy()
    {
        _walkController.OnRightStep -= ActivateHeadbob;
    }
}