using Zenject;

public class LeftStepHeadbob : MovementHeadbob
{
    [Inject] private readonly WalkController _walkController;

    protected new void Awake()
    {
        base.Awake();
        _walkController.OnLeftStep += ActivateHeadbob;
    }

    private void OnDestroy()
    {
        _walkController.OnLeftStep -= ActivateHeadbob;
    }
}