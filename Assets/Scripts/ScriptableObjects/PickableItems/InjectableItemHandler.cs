using Zenject;

public class InjectableItemHandler : PickableItemHandler, IInjectable
{
    [Inject] readonly PickableItemsInventory m_pickableItemsInventory;

    int m_numOfUses;
    bool m_isInjectUsed;

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

    void Awake()
    {
        m_numOfUses = (m_pickableItem_SO as IInjectable).NumOfUses;
    }

    public override void Clicked(int slotIndex)
    {
        if (m_isInjectUsed) { return; }

        base.Clicked(slotIndex);
        m_numOfUses -= m_numOfUses;
    }

    public int NumOfUses => m_numOfUses;

    public void Inject()
    {
        (m_pickableItem_SO as IInjectable).Inject();
        m_isInjectUsed = true;
    }
}