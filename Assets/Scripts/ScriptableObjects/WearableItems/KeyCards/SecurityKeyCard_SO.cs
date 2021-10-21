using UnityEngine;

[CreateAssetMenu(fileName = "new SecurityKeyCard_SO", menuName = "ScriptableObjects/WearableItems/KeyCards/SecurityKeyCard_SO")]
public class SecurityKeyCard_SO : KeyCard_SO
{
    [SerializeField] SecurityKeyCardLevel m_securityKeyCardLevel;

    public enum SecurityKeyCardLevel
    {
        First,
        Second,
        Third,
    }

    public override int KeyCardLevel => (int)m_securityKeyCardLevel;
    public override KeyCardType GetKeyCardType() => KeyCardType.SecurityKeyCard;
}

