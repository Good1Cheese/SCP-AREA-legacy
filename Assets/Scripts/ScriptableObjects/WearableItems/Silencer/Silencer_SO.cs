using UnityEngine;

[CreateAssetMenu(fileName = "new Silencer", menuName = "ScriptableObjects/Silencer")]
public class Silencer_SO : WearableItem_SO
{
    public GameObject silencerForPlayerPrefab;

    public Vector3 positionForSilencer;
}

