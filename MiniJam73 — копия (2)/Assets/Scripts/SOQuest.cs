using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New quest", menuName = "Quest", order = 51)]
public class SOQuest : ScriptableObject
{
    public TypeQuestResource TypeQuestResource;
    public string NameEvent;
    [Header("-Description-")]
    [Space]
    [TextArea()] public string [] LevelOne = new string[5];
    //public List<QuestOption> QuestOptions;
    [TextArea()] public string [] ButtonText;
    [Space]
    [Header("-Dises-")]
    [Range(1, 6)]
    public int [] DiceCheck;

}
public enum TypeQuestResource
{
    DarkPower,
    Hero,
    Minions
    
}

