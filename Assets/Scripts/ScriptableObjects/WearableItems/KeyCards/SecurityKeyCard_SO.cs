
using UnityEngine;

[CreateAssetMenu(fileName = "new SecurityKeyCard", menuName = "ScriptableObjects/SecurityKeyCard")]
public class SecurityKeyCard_SO : KeyCard_SO
{
    public enum TypesOfSecurityCard
    {
        Security, 
        Agent
    }

    public TypesOfSecurityCard TypeOfKeyCard;
}

