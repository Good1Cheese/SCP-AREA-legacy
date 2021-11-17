using UnityEngine;

[CreateAssetMenu(fileName = "new ScienceKeyCard_SO", menuName = "ScriptableObjects/WearableItems/KeyCards/ScienceKeyCard_SO")]
public class ScienceKeyCard_SO : KeyCard_SO
{
    [SerializeField] private ScienceKeyCardLevel _scienceKeyCardLevel;

    public enum ScienceKeyCardLevel
    {
        First,
        Second,
    }

    public override int KeyCardLevel => (int)_scienceKeyCardLevel;
    public override KeyCardType GetKeyCardType()
    {
        return KeyCardType.ScienceKeyCard;
    }
}

