using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState{ roaming, battle};

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSys battleSys;
    [SerializeField] Camera mainCam;
    [SerializeField] AudioClip route1Music;

    GameState state;
    public static GameController instance { get; private set; }

    private void Start()
    {
        playerController.onEncounter += startBattle;
        battleSys.onBattleComplete += endBattle;
    }

    void endBattle(bool won)
    {
        state = GameState.roaming;
        battleSys.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
        AudioController.i.PlayMusic(route1Music);
    }

    void startBattle()
    {
        state = GameState.battle;
        battleSys.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
        battleSys.StartBattle();
    }


    //help with scene loading
    public SceneManage currentScene { get; private set; }
    public SceneManage preScene { get; private set; }

    public void setCurrentScene(SceneManage currScene)
    {
        currentScene = currScene;
    }

    //edit states
    private void Update()
    {
        if (state == GameState.roaming)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.battle)
        {
            battleSys.HandleUpdate();
        }
    }

}
