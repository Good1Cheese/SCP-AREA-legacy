public abstract class KeyCard_SO : WearableIte_SO
{
    public enum KeyCardType
    {
        SecurityKeyCard,
        ScienceKeyCard
    }

    public abstract KeyCardType GetKeyCardType();
    public abstract int KeyCardLevel { get; }
}