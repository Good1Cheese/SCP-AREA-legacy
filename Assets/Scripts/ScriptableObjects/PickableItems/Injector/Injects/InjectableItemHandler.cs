using UnityEngine;

[RequireComponent(typeof(InjectableItemSaving))]
public abstract class InjectableItemHandler : PickableItemHandler, IInjectable
{
    protected int m_numOfUses;

    public int NumsOfUses
    {
        get => m_numOfUses;
        set
        {
            if (m_numOfUses - 1 <= 0)
            {
                m_pickableItemsInventory.RemoveItem(this);
                m_numOfUses = 0;

                return;
            }

            m_numOfUses = value;
        }
    }

    public void SetNumOfUses(int value)
    {
        m_numOfUses = value;
    }

    public void Inject() { }

    protected new void Start()
    {
        m_numOfUses = (m_pickableItem_SO as InjectableItem_SO).NumOfUses;
        base.Start();
    }
}
