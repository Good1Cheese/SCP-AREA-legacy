using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public abstract class Item_SO : ScriptableObject
{
    public Sprite sprite;
    public string description;
}
