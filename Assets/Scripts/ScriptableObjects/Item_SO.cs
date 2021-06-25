using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObjects/Item")]
public abstract class Item_SO : ScriptableObject
{
    public Sprite sprite;

    public GameObject gameobject;

    public abstract void Use();
}
