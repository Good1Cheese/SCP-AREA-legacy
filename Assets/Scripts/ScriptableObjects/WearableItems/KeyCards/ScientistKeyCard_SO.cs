using UnityEngine;

[CreateAssetMenu(fileName = "new ScientistKeyCard", menuName = "ScriptableObjects/ScientistKeyCard")]
public class ScientistKeyCard_SO : KeyCard_SO
{

    public enum TypesOfScientistCard
    {
        FirstLevel,
        SecondLevel, 
        ThirdLevel
    }

    public TypesOfScientistCard TypeOfKeyCard;
}

    
