using UnityEngine;

public class InjectShooter : MonoBehaviour
{
    [SerializeField] KeyCode m_key;

    InjectorReload m_injectorReload;

    void Start()
    {
        m_injectorReload = GetComponent<InjectorReload>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key) || transform.parent == null) { return; }

        Shoot();
    }

    void Shoot()
    {
        if (m_injectorReload.InjectorHandler.ClipInject == null) { return; }

        m_injectorReload.InjectorHandler.ClipInject.Inject();
        m_injectorReload.InjectorHandler.ClipInject = null;
    }
}
