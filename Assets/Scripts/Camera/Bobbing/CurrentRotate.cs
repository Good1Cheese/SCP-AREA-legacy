using System;
using UnityEngine;

public class CurrentRotate
{
    private Quaternion _newRotation = Quaternion.identity;

    private readonly Func<float> _getX;
    private readonly Func<float> _getY;
    private readonly Func<float> _getZ;

    private readonly Transform _transform;

    private float _oldX;
    private float _oldY;
    private float _oldZ;

    public CurrentRotate(Func<float> getX, Func<float> getY, Func<float> getZ, Transform transform)
    {
        _getX = getX;
        _getY = getY;
        _getZ = getZ;
        _transform = transform;
    }

    public void Rotate()
    {
        _transform.localRotation = _newRotation;

        Vector3 eulerAngles = _transform.localRotation.eulerAngles;

        _newRotation = Quaternion.Euler(eulerAngles.x + _getX.Invoke() - _oldX,
                                        eulerAngles.y + _getY.Invoke() - _oldY,
                                        eulerAngles.z + _getZ.Invoke() - _oldZ);
        _oldX = _getX.Invoke();
        _oldY = _getY.Invoke();
        _oldZ = _getZ.Invoke();
    }
}