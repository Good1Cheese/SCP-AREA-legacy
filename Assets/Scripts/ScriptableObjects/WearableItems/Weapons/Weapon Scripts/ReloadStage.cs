using UnityEngine;
using Zenject;

public abstract class ReloadStage : WeaponScriptBase
{
    private Animator _animator;

    public bool Done { get; set; }

    [Inject]
    private void Construct(Animator animator)
    {
        _animator = animator;
    }
}