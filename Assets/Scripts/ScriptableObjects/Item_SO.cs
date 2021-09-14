using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public abstract class Item_SO : ScriptableObject
{
    public Sprite sprite;
    public GameObject gameObject;
    public string description;

    public bool IsInInventory;

    public virtual void OnDestroy()
    {
        IsInInventory = false;
    }

    public abstract void GetDependencies(PlayerInstaller playerInstaller);

    public abstract void Equip();
}
