using UnityEngine;

public class InjectorInjectTypeSwitcher : MonoBehaviour
{
    [SerializeField] KeyCode m_key;

    InjectorReloader m_injectorReloader;

    void Start()
    {
        m_injectorReloader = GetComponent<InjectorReloader>();    
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key)) { return; }

        SwitchInjectType();
    }

    void SwitchInjectType()
    {
        if (m_injectorReloader.InjectType == typeof(IHealthInjectable))
        {
            m_injectorReloader.GetAdrenalinInjects();
            return;
        }

        m_injectorReloader.GetHealthInjects();
    }
}
