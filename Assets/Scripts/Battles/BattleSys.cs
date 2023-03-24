using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum State { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSys : MonoBehaviour
{
    [SerializeField] InPlay player;
    [SerializeField] battleHUD playerHUD;
    [SerializeField] InPlay enemy;
    [SerializeField] battleHUD enemyHUD;
    [SerializeField] BattleDialogue dialogue;
    [SerializeField] GameObject fightButton;
    [SerializeField] GameObject moveButton;
    [SerializeField] GameObject moveButton1;
    [SerializeField] GameObject moveButton2;
    [SerializeField] GameObject moveButton3;

    [SerializeField] AudioClip wildBattleMusic;
    [SerializeField] AudioClip wildBattleVicory;

    public event Action<bool> onBattleComplete;

    State state;
    public int currentMove = 0;

    public void StartBattle()
    {
        AudioController.i.PlayMusic(wildBattleMusic);

        StartCoroutine(SetBattle());
    }

    public IEnumerator SetBattle()
    {
        player.SetUp();
        enemy.SetUp();
        playerHUD.setData(player.Monster);
        enemyHUD.setData(enemy.Monster);
        
        yield return dialogue.TypeDialogue($"A wild {enemy.Monster.Base.Name} appeared.");
        yield return new WaitForSeconds(2f);

        PlayerAction();
    }

    public void PlayerAction()
    {
        state = State.PlayerAction;
        StartCoroutine(dialogue.TypeDialogue($"What will {player.Monster.Base.Name} do?"));
        dialogue.EnableActions(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(fightButton);

    }

    public void HandleUpdate()
    {
        if (state == State.PlayerMove)
        {
            HandleMoveSelect();
        }
    }

    void HandleMoveSelect()
    {
        GameObject sel = EventSystem.current.currentSelectedGameObject;
        if (sel == moveButton)
            currentMove = 0;
        else if (sel == moveButton1)
            currentMove = 1; 
        else if (sel == moveButton2)
            currentMove = 2;
        else if (sel == moveButton3)
            currentMove = 3;
        
        dialogue.UpdateMoves(player.Monster.moves[currentMove]);
    }
    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackMessage());
    }

    IEnumerator PlayerAttackMessage()
    {
        state = State.Busy;
        dialogue.EnableMoveSelect(false);
        dialogue.EnableDialogue(true);
        var move = player.Monster.moves[currentMove];
        yield return dialogue.TypeDialogue($"{player.Monster.Base.Name} used {move.Base.name}.");

        bool isFainted = enemy.Monster.TakeDamage(move, player.Monster);
        enemyHUD.updateHP(enemy.Monster);
        yield return new WaitForSeconds(2f);

        if (isFainted)
        {
            yield return dialogue.TypeDialogue($"The enemy {enemy.Monster.Base.Name} fainted.");

            AudioController.i.PlayMusic(wildBattleVicory);
            yield return new WaitForSeconds(5f);
            onBattleComplete(true);
        }
        else
        {
            StartCoroutine(EnemyAttack());
        }
    }

    IEnumerator EnemyAttack()
    {
        state= State.EnemyMove;
        var move = enemy.Monster.GetRandomMove();

        yield return dialogue.TypeDialogue($"The enemy {enemy.Monster.Base.Name} used {move.Base.name}.");

        bool isFainted = player.Monster.TakeDamage(move, enemy.Monster);

        playerHUD.updateHP(player.Monster);
        yield return new WaitForSeconds(2f);

        if (isFainted)
        {
            yield return dialogue.TypeDialogue($"{player.Monster.Base.Name} fainted.");

            yield return new WaitForSeconds(2f);
            onBattleComplete(false);
        }
        else
        {
            PlayerAction();            
        }
    }

    public void PlayerMove()
    {
        state = State.PlayerMove;
        dialogue.EnableActions(false);
        dialogue.EnableDialogue(false);
        dialogue.EnableMoveSelect(true);
        dialogue.setMoves(player.Monster.moves, moveButton1, moveButton2, moveButton3);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(moveButton);

    }

    public void RunAway()
    {
        StartCoroutine(RunAwayMessage());
    }

    IEnumerator RunAwayMessage()
    {
        state = State.Busy;
        dialogue.EnableActions(false);
        dialogue.EnableDialogue(true);

        bool isRunning = player.Monster.CalculateRun(enemy.Monster);
        if (isRunning)
        {
            yield return dialogue.TypeDialogue($"{player.Monster.Base.Name} ran away.");
            yield return new WaitForSeconds(2f);
            onBattleComplete(false);
        }
        else
        {
            yield return dialogue.TypeDialogue($"{player.Monster.Base.Name} failed to run away.");
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyAttack());
        }
    }


}
