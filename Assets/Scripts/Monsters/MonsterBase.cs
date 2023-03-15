using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monsters", menuName = "Monsters/Create new monster")]
public class MonsterBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite sprite;

    [SerializeField] MonsterType type1;
    [SerializeField] MonsterType type2;

    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;
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