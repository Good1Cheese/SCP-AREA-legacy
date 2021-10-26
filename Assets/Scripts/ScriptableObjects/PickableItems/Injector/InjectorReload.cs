using UnityEngine;

[RequireComponent(typeof(InjectTypeSwitch), typeof(InjectShooter))]
public class InjectorReload : MonoBehaviour
{
    [SerializeField] KeyCode m_key;

    InjectTypeSwitch m_injectTypeSwitcher;

    public InjectorHandler InjectorHandler { get; set; } 
    public PickableItemsInventory PickableItemsInventory { get; set; }

    public ItemHandler CurrentInject
    {
        set
        {
            if (value == null) { return; }

            print(value.name);
            InjectableItemHandler injectableItemHandler = (InjectableItemHandler)value;
            injectableItemHandler.NumsOfUses--;

            InjectorHandler.ClipInject = injectableItemHandler;
        }
    }

    void Start()
    {
        m_injectTypeSwitcher = GetComponent<InjectTypeSwitch>();
    }

    void Update()
    {
        if (!Input.GetKeyDown(m_key) || transform.parent == null) { return; }

        Reload();
    }

    void Reload()
    {
        if (m_injectTypeSwitcher.Type == typeof(IHealthInjectable))
        {
            CurrentInject = PickableItemsInventory.GetIem(item => item.GetItem() as IHealthInjectable != null);
            return;
        }

        CurrentInject = PickableItemsInventory.GetIem(item => item.GetItem() as IAdrenalinInjectable != null);
    }
}
