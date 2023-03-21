using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monsters", menuName = "Monsters/Create new monster")]
public class MonsterBase : ScriptableObject
{

    //Create the properties of Monsters 
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;

    [SerializeField] MonsterType type1;
    [SerializeField] MonsterType type2;

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMoves> learnableMoves;

    //Set properties
    public string Name => name;

    public string Description => description;

    public Sprite LeftSprite => leftSprite;
    public Sprite RightSprite => rightSprite;

    public int MaxHP => maxHp;

    public int Attack => attack;

    public int Defense => defense;

    public int SpAttack => spAttack;

    public int SpDefense => spDefense;

    public int Speed => speed;

    public List<LearnableMoves> LearnableMoves => learnableMoves;

}

[System.Serializable]

//Teach moves respective to level
public class LearnableMoves
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base => moveBase;
    public int Level => level;

}

public enum MonsterType
{
    None,
    Normal,
    Fire,
    Water,
    Grass,
    Flying,
    Ground,
    Rock,
    Electric,
    Ice,
    Fighting,
    Poison,
    Psychic,
    Ghost,
    Dragon,
}