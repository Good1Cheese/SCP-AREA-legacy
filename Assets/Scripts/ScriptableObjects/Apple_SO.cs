using UnityEngine;

[CreateAssetMenu(fileName = "new Apple", menuName = "ScriptableObjects/Apple")]
public class Apple_SO : Item_SO
{
    public override void Use()
    {
        Debug.Log("using");
    }
}