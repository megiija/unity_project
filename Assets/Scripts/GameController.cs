using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance { get; private set; }

    //help with scene loading
    public AdditiveScene currentScene { get; private set; }
    public AdditiveScene preScene { get; private set; }

    public void setCurrentScene(AdditiveScene currScene)
    {
        preScene = currentScene;
        currentScene = currScene;
    }
}
