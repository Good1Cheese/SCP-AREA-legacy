public class SecurityDoorInteractable : DoorInteractable
{
    public SecurityKeyCard_SO.SecurityKeyCardLevel m_securityKeyCardLevelToOpen;
    public override int KeyCardLevelToOpen => (int)m_securityKeyCardLevelToOpen;

    public override int KeyCardType => (int)KeyCard_SO.KeyCardType.SecurityKeyCard;
}