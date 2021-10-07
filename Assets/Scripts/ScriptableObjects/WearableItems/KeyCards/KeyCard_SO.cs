using UnityEngine;

[CreateAssetMenu(fileName = "new KeyCard", menuName = "ScriptableObjects/WearableItems/KeyCard")]
public class KeyCard_SO : Item_SO
{
    [SerializeField] KeyCardLevels m_keyCardLevel;

    public int GetKeyCardLevel() => (int)m_keyCardLevel + 1;

    public enum KeyCardLevels
    {
        First, 
        Second,
        Third,
        Fourth,
        Fifth
    }
}