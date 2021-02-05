using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New quest", menuName = "Quest", order = 51)]
public class SOQuest : ScriptableObject
{
    public TypeQuestResource TypeQuestResource;
    public string NameEvent;
    [Header("Description")]
    [Space]
    [TextArea()]
    public string [] LevelOne = new string[3];
    [TextArea()]
    public string [] LevelTwo = new string[2];
    [Space]
    [Range(1, 6)]
    public int [] DiseCheck;
}
public enum TypeQuestResource
{
    DarkPower,
    Hero,
    Minions
    
}
