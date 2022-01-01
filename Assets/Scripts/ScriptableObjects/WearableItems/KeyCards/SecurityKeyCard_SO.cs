using UnityEngine;

[CreateAssetMenu(fileName = "new SecurityKeyCard_SO", menuName = "ScriptableObjects/WearableItems/KeyCards/SecurityKeyCard_SO")]
public class SecurityKeyCard_SO : KeyCard_SO
{
    [SerializeField] private SecurityKeyCardLevel _securityKeyCardLevel;

    public enum SecurityKeyCardLevel
    {
        First,
        Second,
    }

    public override int KeyCardLevel => (int)_securityKeyCardLevel;
    public override KeyCardType GetKeyCardType()
    {
        return KeyCardType.SecurityKeyCard;
    }
}

