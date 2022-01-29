public class SecurityDoorInteractable : DoorInteractable
{
    public SecurityKeyCard_SO.SecurityKeyCardLevel _securityKeyCardLevelToOpen;

    public override int KeyCardLevelToOpen => (int)_securityKeyCardLevelToOpen;
    public override int KeyCardType => (int)KeyCard_SO.KeyCardType.SecurityKeyCard;
}