using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Move", menuName = "Monsters/Create a new move")]
public class MoveBase : ScriptableObject
{

    //Create properties of moves
    [field: SerializeField] public string moveName { get; private set; }
    [field: SerializeField, TextArea]public string description { get; private set; }
    [field: SerializeField] public MonsterType type { get; private set; }
    [field: SerializeField] public int PP { get; private set; }
    [field: SerializeField] public int power { get; private set; }
    [field: SerializeField] public int accuracy { get; private set; }

    public string Description
    {
        get { return description; }
    }

    public MonsterType Type
    {
        get { return type; }
    }
}
