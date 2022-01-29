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
        moveTime = _dynamicFov.CurveTime;
        fov = _mainCamera.fieldOfView;
    }

    public override void Load()
    {
        _dynamicFov.CurveTime = moveTime;
        _mainCamera.fieldOfView = fov;
    }
}