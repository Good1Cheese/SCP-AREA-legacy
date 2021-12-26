using UnityEngine;

[CreateAssetMenu(fileName = "new Injector", menuName = "ScriptableObjects/WearableItems/Injector")]
public class Injector_SO : Utility_SO
{
    public float shotDelay;
    public WaitForSeconds shotTimeout;

    public float reloadDelay;
    public WaitForSeconds reloadTimeout;

    public float injectChangeDelay;
    public WaitForSeconds injectChangeTimeout;
}