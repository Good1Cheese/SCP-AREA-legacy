public abstract class KeyCard_SO : WearableIte_SO
{
    public abstract KeyCardType GetKeyCardType();
    public abstract int KeyCardLevel { get; }

    public enum KeyCardType
    {
        SecurityKeyCard,
        ScienceKeyCard
    }
}