public class ScienceDoorInteractable : DoorInteractable
{
    public ScienceKeyCard_SO.ScienceKeyCardLevel _scienceKeyCardLevelToOpen;
    public override int KeyCardLevelToOpen => (int)_scienceKeyCardLevelToOpen;

    public override int KeyCardType => (int)KeyCard_SO.KeyCardType.ScienceKeyCard;
}
