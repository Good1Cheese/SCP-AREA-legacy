
using Zenject;

public class PlayerRotatorSaving : DataSaving
{
    [Inject] private readonly PlayerRotator _playerRotator;

    public float _yRotation;
    public float _xRotation;

    public override void Save()
    {
        _yRotation = _playerRotator.YRotation;
        _xRotation = _playerRotator.XRotation;
    }

    public override void LoadData()
    {
        _playerRotator.XRotation = _xRotation;
        _playerRotator.YRotation = _yRotation;
    }

}