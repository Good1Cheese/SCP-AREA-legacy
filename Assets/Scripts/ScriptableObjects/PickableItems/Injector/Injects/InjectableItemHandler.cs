using UnityEngine;

[RequireComponent(typeof(InjectableItemSaving))]
public abstract class InjectableItemHandler : PickableItemHandler, IInjectable
{
    protected int _numOfUses;

    public int NumsOfUses
    {
        get => _numOfUses;
        set
        {
            if (_numOfUses - 1 <= 0)
            {
                _pickableItemsInventory.Remove(this);
                _numOfUses = 0;

                return;
            }

            _numOfUses = value;
        }
    }

    public void SetNumOfUses(int value)
    {
        _numOfUses = value;
    }

    public abstract void Inject();

    protected new void Start()
    {
        _numOfUses = (_pickableIte_SO as InjectableIte_SO).NumOfUses;
        base.Start();
    }
}
