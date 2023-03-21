using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    State state;
    

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
        //dialogue.setMoves(player.Monster.moves);

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

    }

    public void PlayerMove()
    {
        state = State.PlayerMove;
        dialogue.EnableActions(false);
        dialogue.EnableDialogue(false);
        dialogue.EnableMoveSelect(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(moveButton);
    }
}
