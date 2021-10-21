public class ScienceDoorInteractable : DoorInteractable
{
    public ScienceKeyCard_SO.ScienceKeyCardLevel m_scienceKeyCardLevelToOpen;
    public override int KeyCardLevelToOpen => (int)m_scienceKeyCardLevelToOpen;

    public override int KeyCardType => (int)KeyCard_SO.KeyCardType.ScienceKeyCard;
}
