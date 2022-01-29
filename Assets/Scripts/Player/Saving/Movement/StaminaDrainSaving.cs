using Zenject;

public class StaminaDrainSaving : DataSaving
{
    [Inject] private readonly StaminaDrain _staminaDrain;

    public float passedTime;
    public float drainPerSecond;

    public override void Save()
    {
        passedTime = _staminaDrain.PassedTime;
        drainPerSecond = _staminaDrain.DrainPerSecond;
    }

    public override void Load()
    {
        _staminaDrain.DrainPerSecond = drainPerSecond;
        _staminaDrain.PassedTime = passedTime;
    }
}