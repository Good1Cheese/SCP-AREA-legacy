using UnityEngine;

public class InjectShooter : InjectorScriptBase
{
    InjectorReload m_injectorReload;

    new void Start()
    {
        base.Start();
        m_injectorReload = GetComponent<InjectorReload>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key)) { return; }

        Shoot();
    }

    void Shoot()
    {
        if (m_injectorReload.InjectorHandler.ClipInject == null) { return; }

        m_injectorReload.InjectorHandler.ClipInject.Inject();
        m_injectorReload.InjectorHandler.ClipInject = null;
    }
}
