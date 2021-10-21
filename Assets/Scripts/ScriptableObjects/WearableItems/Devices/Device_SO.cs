using UnityEngine;

[CreateAssetMenu(fileName = "new Device", menuName = "ScriptableObjects/WearableItems/Device")]
public class Device_SO : Item_SO
{
    public GameObject flashLightPrefab;
    public Vector3 spawnOffset;
}
