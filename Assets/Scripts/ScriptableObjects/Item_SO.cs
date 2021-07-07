using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public abstract class Item_SO : ScriptableObject
{
    public Sprite sprite;
    public GameObject gameobject;

    public Item_SO Item { get; set; }

    public abstract void GetDependencies(PlayerInstaller playerInstaller);

    public abstract void Equip();

    public abstract void Use();
}
