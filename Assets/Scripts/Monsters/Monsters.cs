using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
    
    public bool TakeDamage(Move move, Monsters attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.power * ((float)attacker.Attack / Defense);
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, moves.Count);
        return moves[r];
    }

    public bool CalculateRun(Monsters enemy)
    {
        float a = Speed;
        float b = enemy.Speed / 4;

        float f = (((a * 32) / b) + 30) % 256;
        Debug.Log(f);

        if (f >= 256)
        {
            return true;
        }
        else
        {
            float chance = Random.Range(0, 256);
            Debug.Log(chance);  
            if (f >= chance)
                return true;
            else
                return false;
        }


    }
}
