using UnityEngine;
using Zenject;

public class FovSaving : DataSaving
{
    [Inject] private readonly DynamicFov _dynamicFov;
    [Inject] private readonly Camera _mainCamera;

    public float moveTime;
    public float fov;

    public override void Save()
    {
        moveTime = _dynamicFov.MoveTime;
        fov = _mainCamera.fieldOfView;
    }

    public override void LoadData()
    {
        _dynamicFov.MoveTime = moveTime;
        _mainCamera.fieldOfView = fov;
    }
}
