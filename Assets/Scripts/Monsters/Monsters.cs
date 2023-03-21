using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Monsters
{
    public MonsterBase Base { get; set; }
    public int Level { get; set; }

    public int HP { get; set; }
    public List<Move> moves { get; set; }

    public Monsters(MonsterBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHP;

        //Check for new moves at level up
        moves = new List<Move>();
        foreach(var move in Base.LearnableMoves)
        {
            if (move.Level <= pLevel)            
                moves.Add(new Move(move.Base));

            if (moves.Count >= 4)
                break;
        }
    }
    
    //Use monsterbase to calcualte current stats based on level
    public int MaxHP => Mathf.FloorToInt(2 * Base.MaxHP * (Level / 100f) + 10 + Level);
  
    public int Attack => Mathf.FloorToInt(2 * Base.Attack * (Level / 100f) + 5);

    public int Defense => Mathf.FloorToInt(2 * Base.Defense * (Level / 100f) + 5);

    public int SpAttack => Mathf.FloorToInt(2 * Base.SpAttack * (Level / 100f) + 5);

    public int SpDefense => Mathf.FloorToInt(2 * Base.SpDefense * (Level / 100f) + 5);

    public int Speed => Mathf.FloorToInt(2 * Base.Speed * (Level / 100f) + 5);
}
