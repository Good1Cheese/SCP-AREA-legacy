using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public class Item_SO : ScriptableObject
{
    public Sprite sprite;
    public GameObject gameobject;

    protected PlayerInventory Inventory { get; set; }
    public Item_SO Item { get; set; }

    public virtual void GetDependencies(PlayerInstaller playerInstaller)
    {
        Inventory = playerInstaller.PlayerInventory;
    }

    public virtual void Equip()
    {
        Inventory.AddItem(this);
    }

    public virtual void Use()
    {
        
    }
}
