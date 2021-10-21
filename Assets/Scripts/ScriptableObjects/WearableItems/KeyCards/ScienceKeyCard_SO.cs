using UnityEngine;

[CreateAssetMenu(fileName = "new ScienceKeyCard_SO", menuName = "ScriptableObjects/WearableItems/KeyCards/ScienceKeyCard_SO")]
public class ScienceKeyCard_SO : KeyCard_SO
{
    [SerializeField] ScienceKeyCardLevel m_scienceKeyCardLevel;

    public enum ScienceKeyCardLevel
    {
        First,
        Second,
    }

    public override int KeyCardLevel => (int)m_scienceKeyCardLevel;
    public override KeyCardType GetKeyCardType() => KeyCardType.ScienceKeyCard;
}

