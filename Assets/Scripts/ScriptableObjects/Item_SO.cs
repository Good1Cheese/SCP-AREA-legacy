using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public abstract class Item_SO : ScriptableObject
{
    public Sprite sprite;
    public GameObject gameObject;
    public string description;

    public bool IsItemInInventory { get; set; }
    public Item_SO Item { get; set; }

    public abstract void GetDependencies(PlayerInstaller playerInstaller);

    public abstract void Equip();

    public virtual void OnDestroy()
    {

    }
}
