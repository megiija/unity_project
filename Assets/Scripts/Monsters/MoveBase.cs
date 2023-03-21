using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Move", menuName = "Monsters/Create a new move")]
public class MoveBase : ScriptableObject
{

    //Create properties of moves
    [field: SerializeField] public string MoveName { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }
    [field: SerializeField] public MonsterType Type { get; private set; }
    [field: SerializeField] public int PP { get; private set; }
    [field: SerializeField] public int Power { get; private set; }
    [field: SerializeField] public int Accuracy { get; private set; }
}
