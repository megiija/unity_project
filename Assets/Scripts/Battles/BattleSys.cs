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


    State state;
    public int currentMove = 0;

    private void Start()
    {
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

    private void Update()
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
        
        yield return new WaitForSeconds(2f);

        bool isFainted = enemy.Monster.TakeDamage(move, player.Monster);

        if (isFainted)
        {
            yield return dialogue.TypeDialogue($"{enemy.Monster.Base.Name} fainted.");
        }
        else
        {
            StartCoroutine(EnemyAttack());
        }
    }

    IEnumerator EnemyAttack()
    {
        state= State.EnemyMove;
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


}
