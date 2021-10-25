using System;
using System.Collections.Generic;
using UnityEngine;

public class InjectorReloader : MonoBehaviour
{
    [SerializeField] KeyCode m_key;

    public InjectorHandler InjectorHadnler { get; set; }
    public Type InjectType { get; set; }
    public List<ItemHandler> Injects { get; set; }
    public PickableItemsInventory PickableItemsInventory { get; set; }

    void Update()
    {
        if (!Input.GetKeyDown(m_key)) { return; }

        Reload();
    }

    void Reload()
    {
        InjectorHadnler.ClipInject = Injects[Injects.Count - 1].GetItem() as IInjectable;
    }

    public void GetHealthInjects()
    {
        InjectType = typeof(IHealthInjectable);
        Injects = PickableItemsInventory.GetItems(item => item.GetItem() as IHealthInjectable != null);
    }

    public void GetAdrenalinInjects()
    {
        InjectType = typeof(IAdrenalinInjectable);
        Injects = PickableItemsInventory.GetItems(item => item.GetItem() as IAdrenalinInjectable != null);
    }

    void OnEnable()
    {
        if (transform.parent == null) { return; }

        GetHealthInjects();
    }
}
